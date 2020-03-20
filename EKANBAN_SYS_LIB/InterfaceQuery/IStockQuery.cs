using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB.InterfaceQuery
{
    public interface IStockQuery
    {
        StockMeasure GetStockMeasure(int _stockMeasureId);
        StockMeasure GetStockMeasure(Schedule _schedule);

        StockPreparing GetStockPreparing(int _stockMeasureId, int _sequenceNo);

        StockPreparing GetStockPreparing(int _stockPrepareId);

        StockMeasure AddNewStockMeasure(StockMeasure _stockMeasure);
        StockMeasure AddNewStockMeasureByPrepare(Schedule _schedule, int _totalSequence);
        bool AddNewStockPreaparing(OriginalPOsequence _sequence, Schedule _schedule);

        bool UpdateStockMeasure(StockMeasure _stockMeasure);
        bool DeleteStockMeasure(int _stockMeasureId);
        bool DeleteStockMeasure(Schedule _schedule);
        bool DeleteStockPrepareing(Schedule _schedule, int sequenceNo);
    }
}
