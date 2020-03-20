namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OriginalPOsequence")]
    public partial class OriginalPOsequence
    {
        public int id { get; set; }

        public int? Quantity { get; set; }

        public int? SequenceNo { get; set; }

        public int? TotalSequence { get; set; }

        public int? SizeType { get; set; }

        public int? OriginalPO_Id { get; set; }
        public virtual OriginalPO OriginalPO { get; set; }
        public virtual ICollection<OriginalSize> OriginalSizes { get; set; }

        [NotMapped]
        public string BeamCutStatus { get; set; }

        [NotMapped]
        public string EKanbanStatus { get; set; }
    }
}
