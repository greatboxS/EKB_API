namespace SYS_MODELS
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("EKanbanInterface")]
    public partial class EKanbanInterface
    {
        public int id { get; set; }
        public string PO { get; set; }
        public string Line { get; set; }
        public string POqty { get; set; }
        public string CartQty { get; set; }
        public string SequenceNo { get; set; }
        public int? EKanbanDevice_Id { get; set; }
        [JsonIgnore]
        public virtual EKanbanDevice EKanbanDevice { get; set; }
        public int? SysActionCode { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? NetworkStatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<EKanbanComponent> EkanbanComponents { get; set; }
        [JsonIgnore]
        public virtual ICollection<EKanbanLoading> EKanbanLoadings { get; set; }

    }
}
