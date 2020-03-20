using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKANBAN_SYS_LIB;
using SYS_MODELS;
using SYS_MODELS._ENUM;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespSchedule
    {
        BeamCutQuery BeamCutQuery;
        ScheduleQuery ScheduleQuery;
        BuildingQuery BuildingQuery;

        public RespSchedule(IDbName database, int machineId, int workerid)
        {
            BeamCutQuery = new BeamCutQuery(database);
            ScheduleQuery = new ScheduleQuery(database);
            BuildingQuery = new BuildingQuery(database);

            var orders = BeamCutQuery.GetBDeviceOrder(machineId, workerid, DateTime.Now.ToString("dd/MM/yyyy"));
            if (orders == null)
            {
                Exception = new RespException(true, "Order is empty", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                return;
            }

            List<Schedule> schedules = new List<Schedule>();
            foreach (var item in orders)
            {
               var temp = ScheduleQuery.GetSchedule(item);
                if (temp != null)
                    schedules.Add(temp);
            }

            ScheduleList = new List<Schedule_t>();

            foreach (var item in schedules)
            {
                ScheduleList.Add(new Schedule_t(database, item));
            }

            Exception = new RespException(false, "Get Order: OK", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
        }

        public RespSchedule(IDbName database, string line, int month, string poNumber)
        {
            try
            {
                BeamCutQuery = new BeamCutQuery(database);
                ScheduleQuery = new ScheduleQuery(database);
                BuildingQuery = new BuildingQuery(database);

                var productionLine = BuildingQuery.FindProductionLine(line);
                if (productionLine == null)
                {
                    Exception = new RespException(true, "Line name is not found", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                    return;
                }
                DateTime SearchTime = new DateTime(DateTime.Now.Year, month, DateTime.Now.Day);

                var scheduleclass = ScheduleQuery.GetScheduleClass(SearchTime);

                if (scheduleclass == null)
                {
                    Exception = new RespException(true, "Invalid schedule time", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                    return;
                }

                var schedules = ScheduleQuery.GetSchedules(poNumber);

                if (schedules == null)
                {
                    Exception = new RespException(true, "Sequence have no item", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                    return;
                }
                //var temp = schedules.Where(i => i.ScheduleClass_Id == scheduleclass.id);
                //if (temp.Count() > 0)
                //    schedules = temp.ToList();

                List<Schedule> newSchedule = new List<Schedule>();

                foreach (var item in productionLine)
                {
                    schedules.Where(i => i.ProductionLine_Id == item.id);
                    if (schedules.Count() > 0)
                        newSchedule.AddRange(schedules.ToList());
                }
                newSchedule = newSchedule.Take(15).ToList();

                ScheduleList = new List<Schedule_t>();
                foreach (var item in newSchedule)
                {
                    ScheduleList.Add(new Schedule_t(database, item));
                }

                Exception = new RespException(false, "Get schedule: OK", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
            }
        }

        public RespSchedule(IDbName database, string line, string poNumber)
        {
            try
            {
                BeamCutQuery = new BeamCutQuery(database);
                ScheduleQuery = new ScheduleQuery(database);
                BuildingQuery = new BuildingQuery(database);

                var productionLine = BuildingQuery.FindProductionLine(line);
                if (productionLine == null)
                {
                    Exception = new RespException(true, "Line name is not found", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                    return;
                }

                var schedules = ScheduleQuery.GetSchedules(poNumber);

                if (schedules == null)
                {
                    Exception = new RespException(true, "Sequence have no item", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                    return;
                }

                List<Schedule> newSchedule = new List<Schedule>();

                foreach (var item in productionLine)
                {
                    schedules.Where(i => i.ProductionLine_Id == item.id);
                    if (schedules.Count() > 0)
                        newSchedule.AddRange(schedules.ToList());
                }
                newSchedule = newSchedule.Take(15).ToList();

                ScheduleList = new List<Schedule_t>();
                foreach (var item in newSchedule)
                {
                    ScheduleList.Add(new Schedule_t(database, item));
                }

                Exception = new RespException(false, "Get schedule: OK", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
            }
        }

        public RespSchedule(IDbName database, string line, int month, int startId)
        {
            try
            {
                BeamCutQuery = new BeamCutQuery(database);
                ScheduleQuery = new ScheduleQuery(database);
                BuildingQuery = new BuildingQuery(database);

                var productionLine = BuildingQuery.FindProductionLine(line);

                DateTime SearchTime = new DateTime(DateTime.Now.Year, month, DateTime.Now.Day);

                if (productionLine == null)
                {
                    Exception = new RespException(true, "Line name is not found", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                    return;
                }

                var schedules = ScheduleQuery.GetSchedules(SearchTime);

                if (schedules == null)
                {
                    Exception = new RespException(true, $"Can not find the schedule for month:{month}", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
                    return;
                }

                List<Schedule> newSchedule = new List<Schedule>();

                foreach (var item in productionLine)
                {
                    var temp = schedules.Where(i => i.ProductionLine_Id == item.id);
                    if (temp.Count() > 0)
                        newSchedule.AddRange(temp.ToList());
                }

                ScheduleList = new List<Schedule_t>();

                var tempSch = newSchedule.Where(i => i.id > startId).Take(15).ToList();
                if (tempSch.Count() > 0)
                    newSchedule = newSchedule.Where(i => i.id > startId).Take(15).ToList();
                else
                    newSchedule = newSchedule.Take(15).ToList();

                ScheduleList = new List<Schedule_t>();
                foreach (var item in newSchedule)
                {
                    var bInterface = BeamCutQuery.GetBeamInterfaceByScheduleId(item.id);
                    var sch = new Schedule_t(database, item);

                    if (bInterface != null)
                        sch.Cutting = true;

                    ScheduleList.Add(sch);
                }


                Exception = new RespException(false, "Get schedule: OK", EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_SCHEDULE);
            }
        }

        public RespException Exception { get; set; }
        public List<Schedule_t> ScheduleList { get; set; }
    }

    public class Schedule_t
    {
        public Schedule_t(IDbName database, int _id, string _poNumber, int _poQty, bool _finish = false)
        {
            id = _id;
            PoNumber = _poNumber;
            PoQty = _poQty;
            Cutting = _finish;
        }

        public Schedule_t(IDbName database, Schedule _scheule)
        {
            SequenceQuery sequenceQuery = new SequenceQuery(database);
            var po = sequenceQuery.GetOriginalPo(_scheule);
            if (po != null)
            {
                OriginalPoId = po.id;
            }
            id = _scheule.id;
            PoNumber = _scheule.PoNumber;
            PoQty = ShareFuncs.GetInt(_scheule.Quantity);
        }
        public int id { get; set; }
        public string PoNumber { get; set; }
        public int PoQty { get; set; }
        public bool Cutting { get; set; }
        public int OriginalPoId { get; set; }
    }
}