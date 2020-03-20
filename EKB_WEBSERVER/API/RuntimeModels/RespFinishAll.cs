using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using EKANBAN_SYS_LIB;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespFinishAll
    {
        public RespFinishAll(IDbName database, int bInterfaceId, int cutCounter)
        {
            BeamCutQuery BeamCutQuery = new BeamCutQuery(database);
            StockQuery StockQuery = new StockQuery(database);
            EmployeeQuery EmployeeQuery = new EmployeeQuery(database);
            SequenceQuery SequenceQuery = new SequenceQuery(database);
            ScheduleQuery ScheduleQuery = new ScheduleQuery(database);
            ComponentQuery ComponentQuery = new ComponentQuery(database);

            try
            {
                var bInterface = BeamCutQuery.GetBeamInterfaceById(bInterfaceId);
                if (bInterface == null)
                {
                    Exception = new RespException(true, "Can not find BInterface", EKB_SYS_REQUEST.FINISH_ALL);
                    return;
                }

                var clonePo = BeamCutQuery.GetBeamCutPo(ShareFuncs.GetInt(bInterface.BeamCutPo_Id));
                if (clonePo == null)
                {
                    Exception = new RespException(true, "Invalid clone Po Id", EKB_SYS_REQUEST.BEAM_START_CUTTING);
                    return;
                }

                int addQty = 0;
                foreach (var seq in clonePo.BeamCutSeqs)
                {
                    var currentSeq = BeamCutQuery.GetBeamCutSeq(seq.id);
                    if (currentSeq.SequenceNo < bInterface.SequenceNo + bInterface.TotalSequence)
                    {
                        foreach (var size in seq.BeamCutSizes)
                        {
                            addQty += ShareFuncs.GetInt(size.SizeQty - size.CutQty);
                            size.CutQty = size.SizeQty;
                            size.Finished = true;
                            size.LastUpdate = DateTime.Now;
                            BeamCutQuery.UpdateBeamSize(size);
                        }

                        currentSeq.CutQty += addQty;
                        currentSeq.LastUpdate = DateTime.Now;
                        currentSeq.Finish = true;
                        BeamCutQuery.UpdateBeamCutSeq(currentSeq);
                    }
                }

                bInterface.BeamCutCounter += cutCounter;
                bInterface.CuttingQty += addQty;
                bInterface.StopCutTime = DateTime.Now;
                if (!BeamCutQuery.UpdateBeamInterface(bInterface))
                {
                    Exception = new RespException(true, "Can not update BInterface", EKB_SYS_REQUEST.FINISH_ALL);
                    return;
                }
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.FINISH_ALL);
            }
        }

        public RespException Exception { get; set; } = new RespException(false, "Finish all: OK", EKB_SYS_REQUEST.FINISH_ALL);
    }
}