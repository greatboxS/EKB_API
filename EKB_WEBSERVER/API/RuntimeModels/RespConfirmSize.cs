using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using EKANBAN_SYS_LIB;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespConfirmSize
    {
        private BeamCutQuery BeamCutQuery;
        private StockQuery StockQuery;
        private EmployeeQuery EmployeeQuery;
        private SequenceQuery SequenceQuery;
        private ScheduleQuery ScheduleQuery;
        private ComponentQuery ComponentQuery;
        public RespConfirmSize(IDbName database, int bInterfaceId, int sizeId, int sizeQty)
        {
            try
            {
                BeamCutQuery = new BeamCutQuery(database);
                StockQuery = new StockQuery(database);
                EmployeeQuery = new EmployeeQuery(database);
                SequenceQuery = new SequenceQuery(database);
                ScheduleQuery = new ScheduleQuery(database);
                ComponentQuery = new ComponentQuery(database);

                var beamInterface = BeamCutQuery.GetBeamInterfaceById(bInterfaceId);
                if (beamInterface == null)
                {
                    Exception = new RespException(true, "Can not find BInterface", EKB_SYS_REQUEST.BEAM_CONFIRM_SIZE);
                    return;
                }

                var bdevice = BeamCutQuery.GetBeamCutDevice(beamInterface.BeamCutDevice_Id);

                if (bdevice == null)
                {
                    Exception = new RespException(true, "Can not find beam cut device", EKB_SYS_REQUEST.BEAM_CONFIRM_SIZE);
                    return;
                }

                var clonePo = BeamCutQuery.GetBeamCutPo(ShareFuncs.GetInt(beamInterface.BeamCutPo_Id));

                if (clonePo == null)
                {
                    Exception = new RespException(true, "Can not find clone Po", EKB_SYS_REQUEST.BEAM_CONFIRM_SIZE);
                    return;
                }

                var seq1 = BeamCutQuery.GetBeamCutSeq(ShareFuncs.GetInt(beamInterface.BeamCutStartSeq_Id));

                var seq2 = BeamCutQuery.GetBeamCutSeq(ShareFuncs.GetInt(beamInterface.BeamCutStopSeq_Id));

                int cutQty = 0;
                int totalQty = 0;
                int currentQty = 0;
                int addQty = 0;
                int seqTotalQty = 0;
                int seqQty = 0;
                foreach (var seq in clonePo.BeamCutSeqs)
                {
                    if (seq.SequenceNo >= seq1.SequenceNo && seq.SequenceNo <= seq2.SequenceNo)
                    {
                        if (seq.Finish == false)
                        {
                            var sizes = BeamCutQuery.GetBeamCutSize(seq.id);

                            int seqcutQty = ShareFuncs.GetInt(seq.CutQty);
                            foreach (var size in sizes)
                            {
                                if (size.SizeId == sizeId)
                                {
                                    if (size.Finished != true && sizeQty > 0)
                                    {
                                        int oldval = ShareFuncs.GetInt(size.CutQty);

                                        int addval = ShareFuncs.GetInt(size.SizeQty) - oldval;

                                        if (sizeQty >= addval)
                                        {
                                            addval = ShareFuncs.GetInt(size.SizeQty) - oldval;
                                            size.CutQty = size.SizeQty;
                                            size.Finished = true;
                                            seqcutQty += addval;
                                            sizeQty -= addval;

                                            addQty += addval;

                                        }
                                        else
                                        {
                                            size.CutQty += sizeQty;
                                            seqcutQty += sizeQty;
                                            addQty += sizeQty;
                                            sizeQty = 0;
                                        }
                                        size.BeamCutDevice_Id = beamInterface.BeamCutDevice_Id;
                                        size.Worker_Id = beamInterface.Employee_Id;
                                        size.LastUpdate = DateTime.Now;
                                        BeamCutQuery.UpdateBeamSize(size);
                                    }

                                    totalQty += ShareFuncs.GetInt(size.SizeQty);
                                    cutQty += ShareFuncs.GetInt(size.CutQty);
                                }

                                currentQty += ShareFuncs.GetInt(size.CutQty);
                            }
                            seq.BeamCutDevice_Id = beamInterface.BeamCutDevice_Id;
                            seq.Worker_Id = beamInterface.Employee_Id;
                            seq.CutQty = seqcutQty;
                            seq.LastUpdate = DateTime.Now;
                            seq.Finish = seq.CutQty == seq.SequenceQty ? true : false;
                            BeamCutQuery.UpdateBeamCutSeq(seq);
                        }

                        seqTotalQty += ShareFuncs.GetInt(seq.CutQty);
                        seqQty += ShareFuncs.GetInt(seq.SequenceQty);
                    }
                }

                if (seqTotalQty == seqQty)
                {
                    StopCutting = true;
                }

                int oldVal = beamInterface.CuttingQty != null ? (int)beamInterface.CuttingQty : 0;
                beamInterface.LastConfirmSize = DateTime.Now;
                beamInterface.CuttingQty = addQty + oldVal;
                BeamCutQuery.UpdateBeamInterface(beamInterface);

                clonePo.LastUpdate = DateTime.Now;
                clonePo.CuttingQuantity += addQty;
                if (clonePo.CuttingQuantity == clonePo.PoQuantity)
                {
                    clonePo.Finish = true;
                    clonePo.FinishTime = DateTime.Now;
                }

                BeamCutQuery.UpdateBeamCutPo(clonePo);

                var statistic = BeamCutQuery.GetBMachineStatistic(ShareFuncs.GetInt(beamInterface.BeamCutDevice_Id), DateTime.Now);
                if (statistic == null)
                {
                    statistic = BeamCutQuery.AddNewBcutStatistic(ShareFuncs.GetInt(beamInterface.BeamCutDevice_Id), DateTime.Now);
                }

                BDeviceCutTimeRecord record = new BDeviceCutTimeRecord
                {
                    TotalCutTime = 0,
                    BeamCutDevice_Id = bdevice.id,
                    ConfirmQuantity = addQty,
                    BMachineStatistic_Id = statistic.id,
                };

                if (!BeamCutQuery.AddNewBCutTimeRecord(record))
                {
                    Exception = new RespException(true, "Add new record failed", EKB_SYS_REQUEST.BEAM_CONFIRM_SIZE);
                }

                int temp = statistic.TotalCutQty != null ? (int)statistic.TotalCutQty : 0;
                statistic.TotalCutQty = temp + addQty;
                BeamCutQuery.UpdateBStatistic(statistic);

                RespTotalCuttingQty = ShareFuncs.GetInt(beamInterface.CuttingQty);
                SizeId = sizeId;
                CutQty = cutQty;
                Finish = (totalQty == cutQty ? true : false);

                Exception = new RespException(false, "Confirm Size: OK", EKB_SYS_REQUEST.BEAM_CONFIRM_SIZE);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_CONFIRM_SIZE);
            }

        }

        public int RespTotalCuttingQty { get; set; }
        public int SizeId { get; set; }
        public int CutQty { get; set; }
        public bool Finish { get; set; }
        public bool StopCutting { get; set; }
        public RespException Exception { get; set; }
    }
}