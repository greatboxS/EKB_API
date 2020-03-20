using EKANBAN_SYS_LIB;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class ConfirmResponse
    {
        public ConfirmResponse(IDbName _database, int _ekanbanDeviceId, int _confirmQty)
        {
            try
            {
                EKanbanTaskQuery eKanbanTaskQuery = new EKanbanTaskQuery(_database);
                SequenceQuery sequenceQuery = new SequenceQuery(_database);
                ComponentQuery componentQuery = new ComponentQuery(_database);
                StockQuery stockQuery = new StockQuery(_database);
                ScheduleQuery scheduleQuery = new ScheduleQuery(_database);
                SysHistoryQuery sysHistoryQuery = new SysHistoryQuery(_database);


                var Interface = eKanbanTaskQuery.GetLastEKanbanInterface(_ekanbanDeviceId);
                var his = sysHistoryQuery.GetEKanbanHistory(Interface);

                if (Interface.SysActionCode == (int)SYS_MODELS._ENUM.SysActionCode.EKANBAN_CONFIRM_ITEM)
                {
                    ConfirmSuccess = true;
                    EMessage = "EKanban is already conform!";
                    return;
                }

                Interface.SysActionCode = (int)SYS_MODELS._ENUM.SysActionCode.EKANBAN_CONFIRM_ITEM;
                Interface.LastUpdate = DateTime.Now;
                if (!eKanbanTaskQuery.UpdateEKanbanInterface(Interface))
                {
                    ConfirmSuccess = false;
                    EMessage = "EKanban is already conform!";
                    return;
                }

                List<Schedule> schedules = new List<Schedule>();

                foreach (var item in Interface.EKanbanLoadings)
                {
                    var sch = scheduleQuery.GetSchedule(ShareFuncs.GetInt(item.OriginalPo_Id));
                    var Seq = sequenceQuery.GetOriginalSequence(ShareFuncs.GetInt(item.OriginalSequence_Id));
                    if (sch != null)
                    {
                        var stock = stockQuery.GetPrepareStockMeasure(sch);

                        if (stock != null)
                        {
                            StockConfirmSequence stockConfirm = new StockConfirmSequence
                            {
                                FinishTime = DateTime.Now,
                                SequenceNo = Seq.SequenceNo,
                                SequenceQty = Seq.Quantity,
                                StockMeasure_Id = stock.id,
                            };

                            StockMessage = "Update stock message successfully";
                            if (!stockQuery.AddNewStockConfirm(stockConfirm))
                                StockMessage = "An error orcured while updating stock";

                            stock.ConfirmUpdateTime = DateTime.Now;
                            stock.EKanbanConfirmQty += Seq.Quantity;
                            if (!stockQuery.UpdateStockMeasure(stock))
                                StockMessage = "An error orcured while updating stock";
                        }
                    }

                    item.SysActionCode = (int)SYS_MODELS._ENUM.SysActionCode.SEQUENCE_CONFORM;
                    item.LastUpdate = DateTime.Now;
                    item.ConfirmQty = _confirmQty;
                    eKanbanTaskQuery.UpdateEKanbanLoading(item);

                    string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(Interface);
                    his.Data = jsonStr;
                    his.DateTime = DateTime.Now;
                    sysHistoryQuery.UpdateEKanbanHis(his);

                    EKanbanConfirmHis eKanbanConfirmHis = new EKanbanConfirmHis
                    {
                        Conform = true,
                        DateTime = DateTime.Now,
                        EKanbanHis_Id = his.id,
                        OriginalSequence_Id = Seq.id,
                        SequenceNo = Seq.SequenceNo,
                        SequenceQty = Seq.Quantity,
                    };
                    sysHistoryQuery.AddNewConfirmHistory(eKanbanConfirmHis);

                    
                }

                ConfirmSuccess = true;
                EMessage = "EKanban is conform successfully";
            }
            catch (Exception e)
            {
                EMessage = e.Message;
            }
        }


        public bool ConfirmSuccess { get; set; }
        public string EMessage { get; set; }
        public string StockMessage { get; set; }
        public string eop { get; set; } = SYS_MODELS._ENUM.SysActionCode.EKANBAN_CONFIRM_ITEM.ToString();
    }
}