namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    [Table("EKanbanLoading")]
    public partial class EKanbanLoading
    {
        public int id { get; set; }
        public int? OriginalSequence_Id { get; set; }
        public int? OriginalPo_Id { get; set; }
        public int? SequenceQty { get; set; }
        public int? ConfirmQty { get; set; }
        public int? SysActionCode { get; set; }
        public int?  EKanbanInterface_Id { get; set; }

        public DateTime? LastUpdate { get; set; }
        public virtual EKanbanInterface EKanbanInterface { get; set; }
    }
}
