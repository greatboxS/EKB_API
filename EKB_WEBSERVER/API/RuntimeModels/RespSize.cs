using EKANBAN_SYS_LIB;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class Size_t
    {
        public int SizeId { get; set; }
        public int SizeQty { get; set; }
        public int CuttingQty { get; set; }
        public bool Finish { get; set; }
    }

    public class RespSize
    {
        public RespSize(IDbName _database, int clonePoId, int _cloneSeqId, int _totalSequence)
        {
            try
            {
                SequenceQuery sequenceQuery = new SequenceQuery(_database);
                BeamCutQuery beamCutQuery = new BeamCutQuery(_database);

                List<OriginalPOsequence> SeqList = new List<OriginalPOsequence>();

                var CloneSizes = beamCutQuery.GetBeamCutSizes(clonePoId, _cloneSeqId, _totalSequence);

                if (CloneSizes == null)
                {
                    Exception = new RespException(true, "CloneSizes is NULL", EKB_SYS_REQUEST.BEAM_GET_SIZE);
                    return;
                }

                SizeList = new List<Size_t>();
                foreach (var size in CloneSizes)
                {
                    SizeList.Add(new Size_t
                    {
                        SizeId = ShareFuncs.GetInt(size.SizeId),
                        SizeQty = ShareFuncs.GetInt(size.SizeQty),
                        CuttingQty = ShareFuncs.GetInt(size.CutQty),
                        Finish = (size.SizeQty == size.CutQty ? true : false)
                    });
                    RespTotalCuttingQty += ShareFuncs.GetInt(size.CutQty);
                    RespTotalQty += ShareFuncs.GetInt(size.SizeQty);
                }

                Exception = new RespException(false, "Get Size: OK", EKB_SYS_REQUEST.BEAM_GET_SIZE);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_SIZE);
            }
        }

        public RespSize(IDbName _database, int seq1Id, int seq2Id)
        {
            try
            {
                SequenceQuery sequenceQuery = new SequenceQuery(_database);
                BeamCutQuery beamCutQuery = new BeamCutQuery(_database);

                List<OriginalPOsequence> SeqList = new List<OriginalPOsequence>();

                var CloneSizes = beamCutQuery.GetBeamCutSizes(seq1Id, seq2Id);

                if (CloneSizes == null)
                {
                    Exception = new RespException(true, "CloneSizes is NULL", EKB_SYS_REQUEST.BEAM_GET_SIZE);
                    return;
                }

                SizeList = new List<Size_t>();
                foreach (var size in CloneSizes)
                {
                    SizeList.Add(new Size_t
                    {
                        SizeId = ShareFuncs.GetInt(size.SizeId),
                        SizeQty = ShareFuncs.GetInt(size.SizeQty),
                        CuttingQty = ShareFuncs.GetInt(size.CutQty),
                        Finish = (size.SizeQty == size.CutQty ? true : false)
                    });
                    RespTotalCuttingQty += ShareFuncs.GetInt(size.CutQty);
                    RespTotalQty += ShareFuncs.GetInt(size.SizeQty);
                }

                Exception = new RespException(false, "Get Size: OK", EKB_SYS_REQUEST.BEAM_GET_SIZE);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_SIZE);
            }
        }
        public int RespTotalCuttingQty { get; set; }
        public int RespTotalQty { get; set; }
        public List<Size_t> SizeList { get; set; }
        public RespException Exception { get; set; }
    }
}