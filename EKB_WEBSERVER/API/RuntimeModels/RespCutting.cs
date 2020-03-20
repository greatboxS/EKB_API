using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using EKANBAN_SYS_LIB;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespCutting
    {
        private BeamCutQuery BeamCutQuery;
        private StockQuery StockQuery;
        private EmployeeQuery EmployeeQuery;
        private SequenceQuery SequenceQuery;
        private ScheduleQuery ScheduleQuery;
        private ComponentQuery ComponentQuery;
        // start cutting construction
        public RespCutting(IDbName database, int deviceId, int workerId, int clonePoId, int seq1Id, int seq2Id, int binterfaceId)
        {
            BeamCutQuery = new BeamCutQuery(database);
            StockQuery = new StockQuery(database);
            EmployeeQuery = new EmployeeQuery(database);
            SequenceQuery = new SequenceQuery(database);
            ScheduleQuery = new ScheduleQuery(database);
            ComponentQuery = new ComponentQuery(database);
            try
            {
                var worker = EmployeeQuery.GetEmployee(workerId);
                if (worker == null)
                {
                    Exception = new RespException(true, "Invalid worker Id", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                var bdevice = BeamCutQuery.GetBeamCutDevice(deviceId);
                if (bdevice == null)
                {
                    Exception = new RespException(true, "Invalid device Id", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                var clonePo = BeamCutQuery.GetBeamCutPo(clonePoId);
                if (clonePo == null)
                {
                    Exception = new RespException(true, "Invalid clone Po Id", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                var originalPo = SequenceQuery.GetOriginalPo(clonePo.OriginalPo_Id);

                if (originalPo == null)
                {
                    Exception = new RespException(true, "Can not find original po", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                var component = ComponentQuery.GetShoeComponent(clonePo.Component_Id);

                if (component == null)
                {
                    Exception = new RespException(true, "Can not find component", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                var schedule = ScheduleQuery.GetSchedule(originalPo);

                if (schedule == null)
                {
                    Exception = new RespException(true, "Can not find schedule", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                var order = BeamCutQuery.GetBDeviceOrder(originalPo, deviceId);

                var startSeq = BeamCutQuery.GetBeamCutSeq(seq1Id);

                var stopSeq = BeamCutQuery.GetBeamCutSeq(seq2Id);

                if (startSeq == null || stopSeq == null)
                {
                    Exception = new RespException(true, "Invalid clone Seq Id", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                int[] qty = BeamCutQuery.GetTotalSelectSequenceQty(startSeq, stopSeq);

                if (qty == null)
                    qty = new int[] { 0, 0 };

                int bId = binterfaceId;
                BeamCutInterface binterface = BeamCutQuery.GetBeamInterfaceById(binterfaceId);

                int scheduleId = 0;
                if (schedule != null)
                    scheduleId = schedule.id;

                if (binterface == null)
                {
                    binterface = new BeamCutInterface
                    {
                        Employee_Id = workerId,
                        BeamCutDevice_Id = deviceId,
                        BeamCutPo_Id = clonePoId,
                        OriginalPo_Id = clonePo.OriginalPo_Id,
                        BeamCutStartSeq_Id = seq1Id,
                        BeamCutStopSeq_Id = seq2Id,
                        TotalSelectedQty = qty[0],
                        TotalSelectCutQty = qty[1],
                        Schedule_Id = scheduleId,
                        StartSeqNo = startSeq.SequenceNo,
                        StopSeqNo = stopSeq.SequenceNo,
                        StartCutTime = DateTime.Now,
                        CuttingQty = 0,
                        BeamCutCounter = 0,
                        BDeviceOrder_Id = order != null ? order.id : 0,
                    };

                    var beamInterface = BeamCutQuery.AddNewBeamCutInterface(binterface);
                    if (beamInterface == null)
                    {
                        Exception = new RespException(true, "Add new BInterface error", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                        return;
                    }
                    bId = beamInterface.id;
                }

                StockMeasure stockMeasure = StockQuery.GetStockMeasure(schedule);

                if (stockMeasure == null)
                {
                    var stock = StockQuery.AddNewStockMeasure(schedule);
                }

                Exception = new RespException(false, "Start cutting: OK", EKB_SYS_REQUEST.BEAM_START_CUTTING);

                InterfaceId = bId;
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_START_CUTTING);
            }
        }

        // stop cut
        public RespCutting(IDbName database, int binterfaceId, int beamCounter)
        {
            BeamCutQuery = new BeamCutQuery(database);
            StockQuery = new StockQuery(database);
            EmployeeQuery = new EmployeeQuery(database);
            SequenceQuery = new SequenceQuery(database);
            ScheduleQuery = new ScheduleQuery(database);
            ComponentQuery = new ComponentQuery(database);

            try
            {
                var bInterface = BeamCutQuery.GetBeamInterfaceById(binterfaceId);
                if (bInterface == null)
                {
                    Exception = new RespException(true, "Can not find BInterface", EKB_SYS_REQUEST.BEAM_STOP_CUTTING);
                    return;
                }

                bInterface.Finish = true;
                bInterface.BeamCutCounter = beamCounter;
                bInterface.StopCutTime = DateTime.Now;
                if (!BeamCutQuery.UpdateBeamInterface(bInterface))
                {
                    Exception = new RespException(true, "Can not update BInterface", EKB_SYS_REQUEST.BEAM_STOP_CUTTING);
                    return;
                }
                Exception = new RespException(false, "Stop cutting: OK", EKB_SYS_REQUEST.BEAM_STOP_CUTTING);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_STOP_CUTTING);
            }
        }

        public int InterfaceId { get; set; }
        public RespException Exception { get; set; }
    }
}