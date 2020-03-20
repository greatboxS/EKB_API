using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKANBAN_SYS_LIB;
using SYS_MODELS._ENUM;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class PoInfo
    {
        public PoInfo(IDbName _database, Schedule _schedule)
        {
            BuildingQuery buildingQuery = new BuildingQuery(_database);
            id = _schedule.id;
            PoNumber = _schedule.PoNumber;
            Model = _schedule.Model;
            Article = _schedule.Article;
            ModelName = _schedule.ModelName;
            Quantity = _schedule.Quantity != null ? (int)_schedule.Quantity : 0;
            Line = buildingQuery.GetProductionLine(ShareFuncs.GetInt(_schedule.ProductionLine_Id)).LineName;
        }

        public int id { get; set; }
        public string PoNumber { get; set; }
        public string Model { get; set; }
        public string ModelName { get; set; }
        public string Article { get; set; }
        public int Quantity { get; set; }
        public string Line { get; set; }
    }
    public class RespPoInfo
    {
        public RespPoInfo(IDbName _database, int scheduleId, int originalPoId, int componentId)
        {
            try
            {
                ScheduleQuery scheduleQuery = new ScheduleQuery(_database);
                SequenceQuery sequenceQuery = new SequenceQuery(_database);
                BeamCutQuery beamCutQuery = new BeamCutQuery(_database);
                SequenceQuery SequenceQuery = new SequenceQuery(_database);
                ComponentQuery componentQuery = new ComponentQuery(_database);

                var schedule = scheduleQuery.GetSchedule(scheduleId);

                var originalPo = SequenceQuery.GetOriginalPo(originalPoId);

                var shoeComponent = componentQuery.GetShoeComponent(componentId);

                if (schedule == null)
                {
                    if (originalPo == null)
                    {
                        Exception = new RespException(true, "Can not find schedule", EKB_SYS_REQUEST.BEAM_GET_PO_INFO);
                        return;
                    }

                    schedule = scheduleQuery.GetSchedule(originalPo);

                    if (schedule == null)
                    {
                        Exception = new RespException(true, "Can not find schedule", EKB_SYS_REQUEST.BEAM_GET_PO_INFO);
                        return;
                    }
                }

                if(shoeComponent==null)
                {
                    Exception = new RespException(true, "Can not find component", EKB_SYS_REQUEST.BEAM_GET_PO_INFO);
                    return;
                }

                PoInfo = new PoInfo(_database, schedule);

                if (originalPo == null)
                {
                    Exception = new RespException(true, "Empty sequence file", EKB_SYS_REQUEST.BEAM_GET_PO_INFO);
                    return;
                }

                var clonePo = beamCutQuery.FindClonePo(originalPo.id, componentId);

                if (clonePo != null)
                {
                    ClonePoId = clonePo.id;

                    SeqList = new List<SeqInfo>();
                    foreach (var item in clonePo.BeamCutSeqs)
                    {
                        SeqList.Add(new SeqInfo
                        {
                            id = item.id,
                            CuttingQty = ShareFuncs.GetInt(item.CutQty),
                            SeqNo = ShareFuncs.GetInt(item.SequenceNo),
                            SeqQty = ShareFuncs.GetInt(item.SequenceQty),
                            Finish = item.Finish == true ? true : false,
                        });
                    }
                }
                else
                {
                    var bClonePo = beamCutQuery.CloneOriginalPo(originalPo, shoeComponent);

                    if (bClonePo == null)
                    {
                        Exception = new RespException(true, "An error occured while requesting po information", EKB_SYS_REQUEST.BEAM_GET_PO_INFO);
                        return;
                    }

                    ClonePoId = bClonePo.id;

                    SeqList = new List<SeqInfo>();
                    foreach (var item in bClonePo.BeamCutSeqs)
                    {
                        SeqList.Add(new SeqInfo
                        {
                            id = item.id,
                            CuttingQty = ShareFuncs.GetInt(item.CutQty),
                            SeqNo = ShareFuncs.GetInt(item.SequenceNo),
                            SeqQty = ShareFuncs.GetInt(item.SequenceQty),
                            Finish = item.Finish == true ? true : false,
                        });
                    }
                }

                Exception = new RespException(false, "Get Poinfo: OK", EKB_SYS_REQUEST.BEAM_GET_PO_INFO);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_PO_INFO);
            }
        }

        public int ClonePoId { get; set; }
        public PoInfo PoInfo { get; set; }
        public List<SeqInfo> SeqList { get; set; }
        public RespException Exception { get; set; }
    }

    public class SeqInfo
    {
        public SeqInfo()
        {

        }
        public SeqInfo(OriginalPOsequence _seq, int _cuttingQty, bool _finish)
        {
            id = _seq.id;
            SeqNo = ShareFuncs.GetInt(_seq.SequenceNo);
            SeqQty = ShareFuncs.GetInt(_seq.Quantity);
            Finish = _finish;
            CuttingQty = _cuttingQty;
        }

        public int id { get; set; }
        public int SeqNo { get; set; }
        public int SeqQty { get; set; }
        public int CuttingQty { get; set; }
        public bool Finish { get; set; }
    }

}