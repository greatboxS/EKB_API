namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockAutoCutSequence")]
    public partial class StockAutoCutSequence
    {
        public int id { get; set; }

        public int? StockMeasure_Id { get; set; }

        public int? SequenceNo { get; set; }

        public int? SequenceQty { get; set; }

        public DateTime? FinishTime { get; set; }
        public virtual StockMeasure StockMeasure { get; set; }

    }
}
