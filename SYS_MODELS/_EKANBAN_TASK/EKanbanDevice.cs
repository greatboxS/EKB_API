namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("EKanbanDevice")]
    public partial class EKanbanDevice
    {
        public int id { get; set; }
        public int? Building_Id { get; set; }
        public int? PropductionLine_Id { get; set; }
        public int? EKanbanCode { get; set; }
        public string Name { get; set; }
        public int? NetworkStatus { get; set; }
        public bool?  ScreenOff { get; set; }
        public DateTime? LastOnline { get; set; }
        public virtual ICollection<EKanbanInterface> EKanbanInterfaces { get; set; }

        [NotMapped]
        public List<EKanbanHis> EKanbanHis { get; set; }
    }
}
