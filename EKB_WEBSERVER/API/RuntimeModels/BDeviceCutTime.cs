using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using EKANBAN_SYS_LIB;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class BDeviceCutTime
    {
        private BeamCutQuery BeamCutQuery;
        public BDeviceCutTime(IDbName database, int deviceId, int totalCutTime)
        {
            BeamCutQuery = new BeamCutQuery(database);

            var statistic = BeamCutQuery.GetBMachineStatistic(deviceId, DateTime.Now);

            if (statistic == null)
            {
                statistic = BeamCutQuery.AddNewBcutStatistic(deviceId, DateTime.Now);
            }

            if (statistic.BDeviceCutTimeRecords != null)
            {
                foreach (var item in statistic.BDeviceCutTimeRecords)
                {
                    TotalCutTime += ShareFuncs.GetInt(item.TotalCutTime);
                }
            }

            BDeviceCutTimeRecord record = new BDeviceCutTimeRecord
            {
                TotalCutTime = totalCutTime,
                BeamCutDevice_Id = deviceId,
                BMachineStatistic_Id = statistic.id,
            };

            if (!BeamCutQuery.AddNewBCutTimeRecord(record))
            {
                Exception = new RespException(true, "Add new record failed", EKB_SYS_REQUEST.CUT_TIME_RECORD);
                return;
            }

            TotalCutTime += totalCutTime;
        }
        public int TotalCutTime { get; set; }
        public RespException Exception { get; set; } = new RespException(false, "Record: OK", EKB_SYS_REQUEST.CUT_TIME_RECORD);
    }
}