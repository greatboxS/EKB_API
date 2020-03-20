using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS_MODELS
{
    [Table("BeamCutPo")]
    public class BeamCutPo
    {
        [Key]
        public int id { get; set; }
        public int OriginalPo_Id { get; set; }
        public int Component_Id { get; set; }
        public string ComponentRef { get; set; }
        public string PoNumber { get; set; }
        public int? PoQuantity { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? FinishTime { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public bool? Finish { get; set; }
        public int? ProductionLine_Id { get; set; }
        public int? CuttingQuantity { get; set; }
        public virtual List<BeamCutSeq> BeamCutSeqs { get; set; }
        public virtual ICollection<BeamCutInterface> BeamCutInterfaces { get; set; }
    }
}
