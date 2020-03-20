using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using EKANBAN_SYS_LIB;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class BLastCut
    {
        private BeamCutQuery BeamCutQuery;
        private StockQuery StockQuery;
        private EmployeeQuery EmployeeQuery;
        private SequenceQuery SequenceQuery;
        private ScheduleQuery ScheduleQuery;
        private ComponentQuery ComponentQuery;
        public BLastCut(IDbName database, int bdeviceId, int bInterfaceId)
        {
            BeamCutQuery = new BeamCutQuery(database);
            StockQuery = new StockQuery(database);
            EmployeeQuery = new EmployeeQuery(database);
            SequenceQuery = new SequenceQuery(database);
            ScheduleQuery = new ScheduleQuery(database);
            ComponentQuery = new ComponentQuery(database);

            try
            {
                var bInterface = BeamCutQuery.GetLastInterface(bdeviceId);

                if (bInterface == null)
                {
                    Exception = new RespException(true, "Can not find BInterface", EKB_SYS_REQUEST.BEAM_GET_LAST_CUT);

                    bInterface = BeamCutQuery.GetBeamInterfaceById(bInterfaceId);
                    if (bInterface == null)
                    {
                        Exception = new RespException(true, "Can not find BInterface", EKB_SYS_REQUEST.BEAM_GET_LAST_CUT);
                        return;
                    }
                }

                var bpo = BeamCutQuery.GetBeamCutPo(ShareFuncs.GetInt(bInterface.BeamCutPo_Id));

                if (bpo == null)
                {

                    Exception = new RespException(true, "Can not find clone Po", EKB_SYS_REQUEST.BEAM_GET_LAST_CUT);
                    return;
                }

                var originalPo = SequenceQuery.GetOriginalPo(ShareFuncs.GetInt(bInterface.OriginalPo_Id));

                if (originalPo == null)
                {
                    Exception = new RespException(true, "Can not find Po", EKB_SYS_REQUEST.BEAM_GET_LAST_CUT);
                    return;
                }

                BinterfaceId = bInterface.id;
                PoNumber = originalPo.PoNumber;
                StartSeq = ShareFuncs.GetInt(bInterface.StartSeqNo);
                StopSeq = ShareFuncs.GetInt(bInterface.StopSeqNo);

                if (bInterface.StartCutTime != null)
                    StartTime = ((DateTime)bInterface.StartCutTime).ToString("hh:mm tt, dd/MM/yyyy");

                if (bInterface.StopCutTime != null)
                    StopTime = ((DateTime)bInterface.StopCutTime).ToString("hh:mm tt, dd/MM/yyyy");

                CutQty = ShareFuncs.GetInt(bInterface.CuttingQty);
                CutTime = ShareFuncs.GetInt(bInterface.BeamCutCounter);
                Employee_Id = ShareFuncs.GetInt(bInterface.Employee_Id);
                Schedule_Id = ShareFuncs.GetInt(bInterface.Schedule_Id);
                BeamCutPo_Id = ShareFuncs.GetInt(bInterface.BeamCutPo_Id);
                BeamCutStartSeq_Id = ShareFuncs.GetInt(bInterface.BeamCutStartSeq_Id);
                Component_Id = ShareFuncs.GetInt(bpo.Component_Id);
                BeamCutStopSeq_Id = ShareFuncs.GetInt(bInterface.BeamCutStopSeq_Id);
                Finish = bInterface.Finish != null ? (bool)bInterface.Finish : false;
                PoQty = ShareFuncs.GetInt(originalPo.Quantity);
                OriginalPo_Id = ShareFuncs.GetInt(bInterface.OriginalPo_Id);

                var component = ComponentQuery.GetShoeComponent(ShareFuncs.GetInt(bpo.Component_Id));
                if (component != null)
                {
                    string comp = ShareFuncs.ConvertToUnSign(component.Reference);
                    Component = comp;
                }
                Exception = new RespException(false, "Get last cut: OK", EKB_SYS_REQUEST.BEAM_GET_LAST_CUT);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_LAST_CUT);
            }
        }

        public int BinterfaceId { get; set; }
        public string PoNumber { get; set; }
        public int PoQty { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string Component { get; set; }
        public int CutQty { get; set; }
        public int CutTime { get; set; }
        public int Employee_Id { get; set; }
        public int Schedule_Id { get; set; }
        public int OriginalPo_Id { get; set; }
        public int BeamCutPo_Id { get; set; }
        public int Component_Id { get; set; }
        public int BeamCutStartSeq_Id { get; set; }
        public int BeamCutStopSeq_Id { get; set; }
        public int StartSeq { get; set; }
        public int StopSeq { get; set; }
        public bool Finish { get; set; }
        public RespException Exception { get; set; }
    }
}