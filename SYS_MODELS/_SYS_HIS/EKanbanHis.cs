namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EKanbanHis")]
    public partial class EKanbanHis
    {
        public int id { get; set; }
        public int? EKanbanDevice_Id { get; set; }
        public int? ProductionLine_Id { get; set; }
        public int? EKanbanInterface_Id { get; set; }
        public string Data { get; set; }
        public DateTime? DateTime { get; set; }
        public virtual ICollection<EKanbanConfirmHis> EKanbanConfirmHis { get; set; }
        public virtual ICollection<EKanbanAddHis> EKanbanAddHis { get; set; }
        public virtual ICollection<EKanbanClearHis> EKanbanClearHis { get; set; }
    }
}
