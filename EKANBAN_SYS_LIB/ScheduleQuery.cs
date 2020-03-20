using EKANBAN_SYS_LIB.InterfaceQuery;
using SCHEDULE;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB
{
    public class ScheduleQuery : IScheduleQuery
    {
        private IDbName database;
        public ScheduleContext ScheduleContext { get; set; }

        public ScheduleQuery(IDbName _database)
        {
            database = _database;
        }

        public bool AddNewFilePath(FilePath _filePath)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.FilePaths.Add(_filePath);
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewScheduleProperties(UpdateSchProperty _updateScheuleProperties)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.UpdateSchProperties.Add(_updateScheuleProperties);
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewScheule(Schedule _schedule)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.Schedules.Add(_schedule);
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewScheuleClass(ScheduleClass _scheduleClass)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.ScheduleClasses.Add(_scheduleClass);
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteAllSchedule(ICollection<Schedule> _schedules)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.Schedules.RemoveRange(_schedules);
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteAllSchedule(ScheduleClass _scheduleClass)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    var schedules = ScheduleContext.Schedules
                         .Where(i => i.ScheduleClass_Id == _scheduleClass.id)
                         .ToList();

                    ScheduleContext.Schedules.RemoveRange(schedules);
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public FilePath GetFilePath(string _type)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.FilePaths.Where(i => i.Type.Contains(_type)).First();
                }
            }
            catch { return null; }
        }

        public Schedule GetSchedule(int _scheduleId)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.Schedules
                        .Include("ScheduleClass")
                        .Where(i => i.id == _scheduleId)
                        .First();
                }
            }
            catch (Exception ex)
            { return null; }
        }

        public ScheduleClass GetScheduleClass(DateTime _scheduleTime)
        {
            try
            {
                string scheduleName = $"{_scheduleTime.Year}{_scheduleTime.Month.ToString("00")}";

                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.ScheduleClasses
                        .Include("UpdateSchProperty")
                        .Where(i => i.Name == scheduleName)
                        .First();
                }
            }
            catch { return null; }
        }

        public ScheduleClass GetScheduleClass(int _scheduleClassId)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.ScheduleClasses
                        .Include("UpdateSchProperty")
                        .Where(i => i.id == _scheduleClassId)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<Schedule> GetSchedules(ProductionLine _productionLine)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.Schedules
                        .Where(i => i.ProductionLine_Id == _productionLine.id)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<Schedule> GetSchedules(ScheduleClass _scheduleClass)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.Schedules
                        .Where(i => i.ScheduleClass_Id == _scheduleClass.id)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<Schedule> GetSchedules(DateTime _scheduleTime)
        {
            try
            {
                var scheduleClass = GetScheduleClass(_scheduleTime);
                if (scheduleClass == null)
                    return null;

                return GetSchedules(scheduleClass);
            }
            catch { return null; }
        }

        public Schedule GetSchedule(OriginalPO _originalPo)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.Schedules
                        .Where(i => i.PoNumber == _originalPo.PoNumber
                        //&& i.ModelName.ToUpper().Trim() == _originalPo.ModelName.ToUpper().Trim()
                        && i.ProductionLine_Id == _originalPo.ProductionLine_Id
                        && i.Quantity == _originalPo.Quantity)
                        //&& i.Article.ToUpper().Trim() == _originalPo.Article.ToUpper().Trim())
                        .First();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null; }
        }

        public UpdateSchProperty GetUpdateScheduleProperties(int _scheduleClassId)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    var scheduleClass = GetScheduleClass(_scheduleClassId);
                    if (scheduleClass == null)
                        return null;
                    return scheduleClass.UpdateSchProperty;
                }
            }
            catch { return null; }
        }

        public bool UpdateScheule(Schedule _schedule)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.Entry(_schedule).State = EntityState.Modified;
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateScheuleClass(ScheduleClass _scheduleClass)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.Entry(_scheduleClass).State = EntityState.Modified;
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateScheuleProperties(UpdateSchProperty _updateSchProperty)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    ScheduleContext.Entry(_updateSchProperty).State = EntityState.Modified;
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public ScheduleClass GetScheduleClass(string _scheduleName)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.ScheduleClasses
                        .Include("UpdateSchProperty")
                        .Where(i => i.Name == _scheduleName)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<Schedule> GetSchedules(string _poNumber)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.Schedules
                        .Where(i => i.PoNumber.Contains(_poNumber))
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<ScheduleClass> GetScheduleClasses()
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.ScheduleClasses.ToList();
                }
            }
            catch { return null; }
        }

        public Schedule GetSchedule(Schedule schedule)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    schedule =  ScheduleContext.Schedules.Where(
                        i => i.PoNumber == schedule.PoNumber &&
                        i.Model == schedule.Model &&
                        i.ModelName == schedule.ModelName &&
                        i.Article == schedule.Article &&
                        i.Quantity == schedule.Quantity &&
                        i.ProductionLine_Id == schedule.ProductionLine_Id &&
                        i.ScheduleClass_Id == schedule.ScheduleClass_Id
                        ).First();
                    return schedule;
                }
            }
            catch { return null; }
        }

        public bool DeletSchedule(Schedule schedule)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    var sche = schedule = ScheduleContext.Schedules.Where(
                        i => i.PoNumber == schedule.PoNumber &&
                        i.Model == schedule.Model &&
                        i.ModelName == schedule.ModelName &&
                        i.Article == schedule.Article &&
                        i.Quantity == schedule.Quantity &&
                        i.ProductionLine_Id == schedule.ProductionLine_Id).First();
                    if (sche == null)
                        return false;

                    ScheduleContext.Entry(sche).State = EntityState.Deleted;
                    ScheduleContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception e)
            { return false; }
        }

        public Schedule GetSchedule(BDeviceOrder bDeviceOrder)
        {
            try
            {
                using (ScheduleContext = new ScheduleContext(database))
                {
                    return ScheduleContext.Schedules.Where(
                        i => i.PoNumber == bDeviceOrder.PoNumber &&
                        i.Quantity == bDeviceOrder.PoQty &&
                        i.ProductionLine_Id == bDeviceOrder.ProductionLine_Id
                        //&& i.ScheduleClass_Id == bDeviceOrder.ScheduleClass_Id
                        ).First();
                }
            }
            catch { return null; }
        }
    }
}
