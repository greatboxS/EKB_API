namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EKanbanConfirmHis")]
    public partial class EKanbanConfirmHis
    {
        public int id { get; set; }
        public int? EKanbanHis_Id { get; set; }
        public virtual EKanbanHis EKanbanHis { get; set; }
        public int? OriginalSequence_Id { get; set; }
        public int? SequenceNo { get; set; }
        public int? SequenceQty { get; set; }
        public bool? Conform { get; set; }
        public DateTime? DateTime { get; set; }
    }

    [Table("EKanbanAddHis")]
    public partial class EKanbanAddHis
    {
        public int id { get; set; }
        public int? EKanbanHis_Id { get; set; }
        public virtual EKanbanHis EKanbanHis { get; set; }
        public int? OriginalSequence_Id { get; set; }
        public int? SequenceNo { get; set; }
        public int? SequenceQty{ get; set; }
        public bool? Added { get; set; }
        public DateTime? DateTime { get; set; }
    }

    [Table("EKanbanClearHis")]
    public partial class EKanbanClearHis
    {
        public int id { get; set; }
        public int? EKanbanHis_Id { get; set; }
        public virtual EKanbanHis EKanbanHis { get; set; }
        public bool? Removed { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
