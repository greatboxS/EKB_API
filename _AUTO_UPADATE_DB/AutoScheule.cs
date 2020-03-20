using EKANBAN_SYS_LIB;
using EKANBAN_SYS_LIB.InterfaceQuery;
using SCHEDULE;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.OleDb;
using EKANBAN_SYS_LIB.OLEDB;

namespace _AUTO_UPADATE_DB
{
    public class AutoScheule
    {
        private IDbName database;
        private IBuildingQuery IBuidingQuery;
        private ScheduleQuery IScheduleQuery;
        public AutoScheule(IDbName _database)
        {
            database = _database;
        }

        public void Update()
        {
            IBuidingQuery = new BuildingQuery(database);
            IScheduleQuery = new ScheduleQuery(database);
            bool success = false;

            FilePath filePath = IScheduleQuery.GetFilePath("schedule");

            if (filePath == null)
                Console.WriteLine("File Path is null");

            IEnumerable<string> folderList = Directory.EnumerateDirectories(filePath.Path);

            foreach (var scheduleFilePath in folderList)
            {
                Console.WriteLine(scheduleFilePath);

                DateTime lastEditFolder = Directory.GetLastWriteTime(scheduleFilePath);

                DirectoryInfo Directorys = new DirectoryInfo(scheduleFilePath);//Assuming Test is your Folder
                FileInfo[] Files = Directorys.GetFiles("*.xlsm"); //Getting Text files

                if (Files.Length == 0)
                {
                    Console.WriteLine("Folder is empty, Continue the next one");
                    continue;
                }

                FileInfo LastFile = Files[Files.Length - 1];
                string folderName = GetScheduleClass(scheduleFilePath);
                // checking folder
                var scheduleClass = IScheduleQuery.GetScheduleClass(folderName);//CheckingFolder(folderName);

                if (scheduleClass != null)
                {
                    Console.WriteLine("Schedule Class is not Null");

                    if (!CheckingFolderModified(scheduleClass, lastEditFolder))
                    {
                        Console.WriteLine("Nothing changed, Countinue!");
                        continue;
                    }
                    scheduleClass.LastModified = lastEditFolder;
                    Console.WriteLine($"Last file: modified time:{scheduleClass.LastModified.ToString()}");

                    var updateSchProperties = scheduleClass.UpdateSchProperty;
                    if (updateSchProperties == null)
                    {
                        //update scheduleclass
                        updateSchProperties = new UpdateSchProperty
                        {
                            FileName = LastFile.Name,
                            CreatedDate = LastFile.CreationTime,
                            ModifiedDate = LastFile.LastWriteTime,
                        };
                        success = IScheduleQuery.AddNewScheduleProperties(updateSchProperties);

                        if (success)
                            Console.WriteLine("Add new schedule class property successfull");
                        else
                            Console.WriteLine("Add new schedule class property error");
                    }
                    else
                    {
                        string time1 = updateSchProperties.ModifiedDate.ToString("MM/dd/yyyy, hh:mm:");
                        string time2 = LastFile.LastWriteTime.ToString("MM/dd/yyyy, hh:mm:");
                        if (time1 == time2)
                        {
                            Console.WriteLine("Nothing changed, Continue!");
                            continue;
                        }
                    }

                    Console.WriteLine("Update Excel file:{0}", LastFile);
                    //update file
                    UpdateScheduleFile(LastFile, scheduleClass);

                    scheduleClass.UpdateSchProperties_Id = updateSchProperties.id;
                    success = IScheduleQuery.UpdateScheuleClass(scheduleClass);

                    if (success)
                        Console.WriteLine("Update schedule class successfully");
                    else
                        Console.WriteLine("Update schedule class error");

                    updateSchProperties.FileName = LastFile.Name;
                    updateSchProperties.CreatedDate = LastFile.CreationTime;
                    updateSchProperties.ModifiedDate = LastFile.LastWriteTime;

                    success = IScheduleQuery.UpdateScheuleProperties(updateSchProperties);

                    if (success)
                        Console.WriteLine("Update schedule class property successfully");
                    else
                        Console.WriteLine("Update schedule class property error");

                    Console.WriteLine("Finished add");
                }
                else
                {
                    Console.WriteLine("Schedule Class is null");
                    // add new folder add new file
                    ScheduleClass schClass = new ScheduleClass
                    {
                        Name = folderName,
                        LastModified = lastEditFolder
                    };

                    success = IScheduleQuery.AddNewScheuleClass(schClass);
                    if (success)
                        Console.WriteLine("Add new schedule class successfully");
                    else
                        Console.WriteLine("Add new schedule class error");

                    //update file
                    UpdateScheduleFile(LastFile, schClass);

                    UpdateSchProperty prop = new UpdateSchProperty
                    {
                        FileName = LastFile.Name,
                        CreatedDate = LastFile.CreationTime,
                        ModifiedDate = LastFile.LastWriteTime
                    };

                    success = IScheduleQuery.AddNewScheduleProperties(prop);

                    if (success)
                        Console.WriteLine("Add new schedule class property successfully");
                    else
                        Console.WriteLine("Add new schedule class property error");

                    schClass.UpdateSchProperties_Id = prop.id;

                    success = IScheduleQuery.UpdateScheuleClass(schClass);

                    if (success)
                        Console.WriteLine("Update schedule class successfully");
                    else
                        Console.WriteLine("Update schedule class error");
                }
            }
        }

        private void UpdateScheduleFile(FileInfo _lastFile, ScheduleClass _scheduleClass)
        {
            try
            {
                Random rd = new Random();
                int rdNum = rd.Next();

                //string newPath = $"C:\\Users\\kha.le\\Desktop\\{rdNum}.xlsm";
                string newPath = $"E:\\{rdNum}.xlsm";

                File.Copy(_lastFile.FullName, newPath);
                DataTable scheduleTb = ExcelHandle.ReadExcelTable(newPath, 0);
                Console.WriteLine("Reading file finished");
                File.Delete(newPath);
                if (scheduleTb == null)
                {
                    Console.WriteLine("Can't read the file");
                    return;
                }

                if (!IScheduleQuery.DeleteAllSchedule(_scheduleClass))
                    return;

                ProductionLine Line = new ProductionLine();
                foreach (DataRow row in scheduleTb.Rows)
                {
                    object Firstcell = row[(int)ColumnMapping.C_CFD];
                    object Pocell = row[(int)ColumnMapping.C_PO];

                    string temp = Pocell != null ? Pocell.ToString().Trim() : "";

                    if (Firstcell != null && temp == "")
                    {
                        if (Firstcell.ToString() != "")
                        {
                            string s = Firstcell.ToString().ToUpper().Replace(" ", "").Trim();
                            try
                            {
                                Line = IBuidingQuery.GetProductionLineByName(s);
                                Console.WriteLine($"Production Line: {Line.LineName}");
                            }
                            catch
                            {
                                Line = null;
                            }
                        }
                    }

                    if (Line == null)
                        continue;

                    Schedule PO = GetOrderInformation(row);

                    if (PO == null)
                        continue;

                    PO.Building_Id = Line.Building_Id;
                    PO.ProductionLine_Id = Line.id;
                    PO.ScheduleClass_Id = _scheduleClass.id;

                    if (IScheduleQuery.GetSchedule(PO) != null)
                        continue;

                    IScheduleQuery.AddNewScheule(PO);

                    Console.WriteLine($"Add new PO, ID: {PO.id}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); ;
            }
        }

        private static Schedule GetOrderInformation(DataRow _row)
        {
            try
            {
                object PO_Cell = _row[(int)ColumnMapping.C_PO];
                if (PO_Cell != null)
                {
                    string po = PO_Cell.ToString();
                    if (po != "")
                    {
                        Schedule PO = new Schedule
                        {
                            PoNumber = ExcelHandle.Excel_GetCellText(_row, (int)ColumnMapping.C_PO),
                            Model = ExcelHandle.Excel_GetCellText(_row, (int)ColumnMapping.C_MODEL_NO),
                            ModelName = ExcelHandle.Excel_GetCellText(_row, (int)ColumnMapping.C_MODEL_NAME),
                            Article = ExcelHandle.Excel_GetCellText(_row, (int)ColumnMapping.C_ARTICLE),
                            CuttingDate = ExcelHandle.Excel_GetCellText(_row, (int)ColumnMapping.C_CUTTING_DATE),
                            StitchindDate = ExcelHandle.Excel_GetCellText(_row, (int)ColumnMapping.C_STITCHING_DATE),
                            Quantity = ExcelHandle.Excel_GetCellNumber(_row, (int)ColumnMapping.C_QUANTITY),
                        };
                        return PO;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        private static bool CheckingFolderModified(ScheduleClass _scheduleClass, DateTime _time)
        {
            if (_scheduleClass.LastModified.ToString("MM/dd/yyyy, hh:mm:") != _time.ToString("MM/dd/yyyy, hh:mm:"))
                return true;

            if (_scheduleClass.UpdateSchProperties_Id == null)
                return true;

            return false;
        }

        private static string GetScheduleClass(string folderPath)
        {
            try
            {
                string[] buf = folderPath.Split(new string[1] { "\\" }, StringSplitOptions.None);
                string name = buf[buf.Length - 1];
                return name;
            }
            catch { return null; }
        }

        private static int MonthSplit(string folderPath)
        {
            try
            {
                string[] buf = folderPath.Split(new string[1] { "\\" }, StringSplitOptions.None);
                string name = buf[buf.Length - 1];
                string m = name.Remove(0, 4);
                int month = int.Parse(m);
                return month;
            }
            catch { return 0; }
        }
    }
}