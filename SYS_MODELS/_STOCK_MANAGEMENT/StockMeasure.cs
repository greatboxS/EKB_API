namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockMeasure")]
    public partial class StockMeasure
    {
        public int id { get; set; }

        public string PoNumber { get; set; }

        public string ModelNumber { get; set; }

        public string ModelName { get; set; }

        public string Article { get; set; }

        public int? PoQuantity { get; set; }

        public int? ProductionLine_Id { get; set; }

        public int? ScheduleClass_Id { get; set; }

        public int? AutoCutQty { get; set; }

        public int? BeamCutQty { get; set; }

        public int? PrepareQty { get; set; }

        public int? EKanbanConfirmQty { get; set; }

        public int? AutoCutCode { get; set; }

        public int? BeamCutCode { get; set; }

        public int? PrepareCode { get; set; }

        public int? ConfirmCode { get; set; }

        public int? TotalSequence { get; set; }

        public DateTime? AutoCutUpdateTime { get; set; }
        public DateTime? BeamCutUpdateTime { get; set; }
        public DateTime? PreparingTime { get; set; }
        public DateTime? ConfirmUpdateTime { get; set; }

        public virtual ICollection<StockPreparing> StockPreparing { get; set; }
        public virtual ICollection<StockAutoCutSequence> StockAutoCutSequences { get; set; }
        public virtual ICollection<StockBeamCutSequence> StockBeamCutSequences { get; set; }
        public virtual ICollection<StockConfirmSequence> StockConfirmSequences { get; set; }

    }
}
