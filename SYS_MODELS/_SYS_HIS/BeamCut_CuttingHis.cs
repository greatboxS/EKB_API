namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("BeamCut_CuttingHis")]
    public partial class BeamCut_CuttingHis
    {
        public int id { get; set; }
        public int? BeamCutHis_Id { get; set; }
        public virtual BeamCutHis BeamCutHis { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public int? StartQty { get; set; }
        public int? StopQty { get; set; }
        public bool? Finished { get; set; }

        public virtual ICollection<BeamCut_ConfirmSizeHis> BeamCut_ConfirmSizeHis { get; set; }
    }
}
