namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("BeamCutSeq")]
    public partial class BeamCutSeq
    {
        public int id { get; set; }
        public int? Component_Id { get; set; }
        public int? BeamCutPo_Id { get; set; }
        public int? OriginalPo_Id { get; set; }
        public virtual BeamCutPo BeamCutPo { get; set; }
        public int? SequenceNo { get; set; }
        public int? SequenceQty { get; set; }
        public int? CutQty { get; set; }
        public bool? Finish { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? BeamCutDevice_Id { get; set; }
        public virtual BeamCutDevice BeamCutDevice { get; set; }
        public int? Worker_Id { get; set; }
        public virtual List<BeamCutSize> BeamCutSizes { get; set; }
        public virtual List<BeamCutInterface> BeamCutInterfaces { get; set; }

    }
}
