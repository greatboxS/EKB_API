using EKANBAN_SYS_LIB.InterfaceQuery;
using STOCK_MANAGEMENT;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB
{
    public class StockQuery 
    {
        IDbName database;
        public StockContext StockContext { get; set; }
        public StockQuery(IDbName _database)
        {
            database = _database;
        }

        public StockMeasure AddNewStockMeasure(StockMeasure _stockMeasure)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    var result = StockContext.StockMeasures.Add(_stockMeasure);
                    StockContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public StockMeasure AddNewStockMeasureByPrepare(Schedule _schedule, int _totalSequence)
        {
            try
            {
                StockMeasure stockMeasure = new StockMeasure
                {
                    PoNumber = _schedule.PoNumber,
                    ModelNumber = _schedule.Model,
                    ModelName = _schedule.ModelName,
                    Article = _schedule.Article,
                    PoQuantity = _schedule.Quantity,
                    TotalSequence = _totalSequence,
                    PreparingTime = DateTime.Now,
                    ProductionLine_Id = _schedule.ProductionLine_Id,
                    PrepareCode = (int)SysActionCode.STOCK_START_PREAPARING,
                    ScheduleClass_Id = _schedule.ScheduleClass_Id,
                };

                using (StockContext = new StockContext(database))
                {
                    StockContext.StockMeasures.Add(stockMeasure);
                    StockContext.SaveChanges();
                    return stockMeasure;
                }
            }
            catch { return null; }
        }

        public StockMeasure AddNewStockMeasure(Schedule _schedule)
        {
            try
            {
                StockMeasure stockMeasure = new StockMeasure
                {
                    PoNumber = _schedule.PoNumber,
                    ModelNumber = _schedule.Model,
                    ModelName = _schedule.ModelName,
                    Article = _schedule.Article,
                    PoQuantity = _schedule.Quantity,
                    PreparingTime = DateTime.Now,
                    ProductionLine_Id = _schedule.ProductionLine_Id,
                    ScheduleClass_Id = _schedule.ScheduleClass_Id
                };

                using (StockContext = new StockContext(database))
                {
                    StockContext.StockMeasures.Add(stockMeasure);
                    StockContext.SaveChanges();
                    return stockMeasure;
                }
            }
            catch { return null; }
        }

        public bool AddNewStockPreaparing(OriginalPOsequence _sequence, Schedule _schedule)
        {
            try
            {
                StockMeasure stockMeasure = GetStockMeasure(_schedule);
                if (stockMeasure == null)
                    return false;

                using (StockContext = new StockContext(database))
                {
                    StockContext.StockPreparing.Add(new StockPreparing
                    {
                        SequenceNo = _sequence.SequenceNo,
                        SequenceQty = _sequence.Quantity,
                        StockMeasure_Id = stockMeasure.id,
                        UpdateTime = DateTime.Now

                    });
                    StockContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewStockPreaparing(OriginalPOsequence _sequence, int _stockMeasureId)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    StockContext.StockPreparing.Add(new StockPreparing
                    {
                        SequenceNo = _sequence.SequenceNo,
                        SequenceQty = _sequence.Quantity,
                        StockMeasure_Id = _stockMeasureId,
                        UpdateTime = DateTime.Now,
                    });
                    StockContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteStockMeasure(int _stockMeasureId)
        {
            try
            {
                StockMeasure stockMeasure = GetStockMeasure(_stockMeasureId);
                if (stockMeasure == null)
                    return false;

                using (StockContext = new StockContext(database))
                {
                    StockContext.Entry(stockMeasure).State = EntityState.Deleted;
                    StockContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteStockMeasure(Schedule _schedule)
        {
            try
            {
                StockMeasure stockMeasure = GetStockMeasure(_schedule);
                if (stockMeasure == null)
                    return false;

                using (StockContext = new StockContext(database))
                {
                    StockContext.Entry(stockMeasure).State = EntityState.Deleted;
                    StockContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public StockMeasure GetStockMeasure(int _stockMeasureId)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    return StockContext.StockMeasures.Find(_stockMeasureId);
                }
            }
            catch { return null; }
        }

        public StockMeasure GetStockMeasure(Schedule _schedule)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    return StockContext.StockMeasures.Where(i =>
                    i.ProductionLine_Id == _schedule.ProductionLine_Id
                    && i.PoNumber == _schedule.PoNumber
                    && i.ModelName == _schedule.ModelName
                    && i.ModelNumber == _schedule.Model
                    && i.Article == _schedule.Article
                    && i.PoQuantity == _schedule.Quantity
                    && i.ScheduleClass_Id == _schedule.ScheduleClass_Id
                    ).First();
                }
            }
            catch { return null; }
        }

        public StockMeasure AddNewStockMeasureByPrepare(Schedule _schedule)
        {
            try
            {
                StockMeasure stockMeasure = new StockMeasure
                {
                    PoNumber = _schedule.PoNumber,
                    ModelNumber = _schedule.Model,
                    ModelName = _schedule.ModelName,
                    Article = _schedule.Article,
                    PoQuantity = _schedule.Quantity,
                    PreparingTime = DateTime.Now,
                    ProductionLine_Id = _schedule.ProductionLine_Id,
                    PrepareCode = (int)SysActionCode.STOCK_START_PREAPARING,
                    ScheduleClass_Id = _schedule.ScheduleClass_Id,
                };

                using (StockContext = new StockContext(database))
                {
                    StockContext.StockMeasures.Add(stockMeasure);
                    StockContext.SaveChanges();
                    return stockMeasure;
                }
            }
            catch { return null; }

        }

        public StockMeasure GetPrepareStockMeasure(Schedule _schedule)
        {
            try
            {
                var stockMeasure = GetStockMeasure(_schedule);

                if (stockMeasure != null)
                    return stockMeasure;

                return AddNewStockMeasureByPrepare(_schedule);
            }
            catch { return null; }
        }

        public bool UpdateStockMeasure(StockMeasure _stockMeasure)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    StockContext.Entry(_stockMeasure).State = EntityState.Modified;
                    StockContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteStockPreparing(Schedule _schedule, int sequenceNo)
        {
            try
            {
                var stock = GetStockMeasure(_schedule);

                if (stock == null)
                    return false;

                var EkanbanLoading = GetStockPreparing(stock.id, sequenceNo);

                using (StockContext = new StockContext(database))
                {
                    StockContext.Entry(EkanbanLoading).State = EntityState.Deleted;
                    StockContext.SaveChanges();
                }

                return true;
            }
            catch { return false; }
        }

        public bool DeleteStockPreparing(int _stockMeasureId, int sequenceNo)
        {
            try
            {
                var EkanbanLoading = GetStockPreparing(_stockMeasureId, sequenceNo);
                using (StockContext = new StockContext(database))
                {
                    StockContext.Entry(EkanbanLoading).State = EntityState.Deleted;
                    StockContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public StockPreparing GetStockPreparing(int _stockMeasureId, int _sequenceNo)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    return StockContext.StockPreparing.Where(i => i.StockMeasure_Id == _stockMeasureId
                    && i.SequenceNo == _sequenceNo).First();
                }
            }
            catch { return null; }
        }

        public StockPreparing GetStockPreparing(int _stockPrepareId)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    return StockContext.StockPreparing.Find(_stockPrepareId);
                }
            }
            catch { return null; }
        }

        public bool AddNewStockConfirm(StockConfirmSequence _confirmStock)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    StockContext.StockConfirmSequences.Add(_confirmStock);
                    StockContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public ICollection<StockMeasure> GetStockMeasures(int productionLineId, int scheduleClassId)
        {
            try
            {
                using (StockContext = new StockContext(database))
                {
                    return StockContext.StockMeasures
                        .Where(i => i.ScheduleClass_Id == scheduleClassId && i.ProductionLine_Id == productionLineId)
                        .ToList();
                }
            }
            catch { return null; }
        }
    }
}
