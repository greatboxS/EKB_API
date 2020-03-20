namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("EKanbanComponent")]
    public partial class EKanbanComponent
    {
        public int id { get; set; }
        public int? ShoeComponent_Id { get; set; }
        public virtual EKanbanInterface EKanbanInterface { get; set; }
        public int? EKanbanInterface_Id { get; set; }
        public int? CuttingType_Id { get; set; }

        [NotMapped]
        public CuttingType CuttingType { get; set; }
    }
}
