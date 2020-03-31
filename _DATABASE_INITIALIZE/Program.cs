using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _AUTO_UPADATE_DB;
using EKANBAN_SYS_LIB;
using Newtonsoft.Json;
using EKB_WEBSERVER.API.RuntimeModels;

namespace _DATABASE_INITIALIZE
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbName database = new RealDb();
            SequenceQuery sequenceQuery = new SequenceQuery(database);
            ScheduleQuery ScheduleQuery = new ScheduleQuery(database);

            var po = sequenceQuery.GetOriginalPo(3987);
            var schedule = ScheduleQuery.GetSchedule(po);


            //IDbName database = new FakeDb();
            while (true)
            {
                string s = Console.ReadLine().ToLower();
                if (s.Contains("update-database"))
                {
                    SERVER_INITIALIZE.DbSeed DB = new SERVER_INITIALIZE.DbSeed(database);
                    DB.Runing();
                }

                if (s.Contains("update-schedule"))
                {
                    Console.WriteLine("Start checking...");

                    AutoScheule AutoSchedule = new AutoScheule(database);
                    AutoSchedule.Update();

                    Console.WriteLine("Stop checking...");
                }

                if (s.Contains("get-schedule"))
                {
                    ScheduleQuery scheduleQuery = new ScheduleQuery(database);
                    var schedules = scheduleQuery.GetSchedules("012").Take(5);
                    foreach (var item in schedules)
                    {
                        Console.WriteLine($"Id:{item.id}, Number: {item.PoNumber}");
                    }

                    Console.WriteLine("Please select scheule to clone");
                    string num = Console.ReadLine();
                    int scheduleId = 0;
                    int.TryParse(num, out scheduleId);

                    BeamCutQuery beamCutQuery = new BeamCutQuery(database);
                    var results = beamCutQuery.CloneOriginalPo(scheduleId);

                    if (results != null)
                    {
                        foreach (var item in results)
                        {
                            Console.WriteLine($"Original Id: {item.OriginalPo_Id}, Comp Id: {item.Component_Id}");
                            foreach (var seq in item.BeamCutSeqs)
                            {
                                Console.WriteLine($"Seq Id: {seq.id}, Seq No: {seq.SequenceNo}, Seq Qty: {seq.SequenceQty}");
                                foreach (var size in seq.BeamCutSizes)
                                {
                                    Console.WriteLine($"Size Id: {size.SizeId},Beam Po Id: {size.BeamCutPo_Id}, Beam Seq Id: {size.BeamCutSeq_Id}");
                                }
                            }
                        }
                    }
                }

                if (s.Contains("request-schedule"))
                {
                    var schedules = new RespSchedule(database, "L3", 12, 0);
                    Console.WriteLine(JsonConvert.SerializeObject(schedules));
                }
                if (s.Contains("request-poinfo"))
                {
                    //var schedules = new EKB_WEBSERVER.API.RuntimeModels.RespPoInfo(database,  2541 ,341);
                }

                if (s.Contains("request-size"))
                {
                    var sizes = new RespSize(database, 3, 9, 341);
                    Console.WriteLine(JsonConvert.SerializeObject(sizes));
                }

                if (s.Contains("request-lastcut"))
                {
                    //Console.WriteLine(JsonConvert.SerializeObject(new BLastCut(database, 4)));
                }

                if (s.Contains("info"))
                {
                    //var sizes = new EKB_WEBSERVER.API.RuntimeModels.RespPoInfo(database, 2540, 322);
                    //Console.WriteLine(JsonConvert.SerializeObject(sizes));
                }

                if (s.Contains("cuttime"))
                {
                    var temp = new BDeviceCutTime(database, 1, 6);
                }


            }
        }
    }
}
