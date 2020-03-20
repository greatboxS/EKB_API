namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("BeamCutSize")]
    public partial class BeamCutSize
    {
        public int id { get; set; }
        public int? Component_Id { get; set; }
        public int? BeamCutPo_Id { get; set; }
        public int? BeamCutSeq_Id { get; set; }
        public virtual BeamCutSeq BeamCutSeq { get; set; }

        public int? BeamCutDevice_Id { get; set; }
        public virtual BeamCutDevice BeamCutDevice { get; set; }
        public int? Worker_Id { get; set; }
        public int? SequenceNo { get; set; }
        public int? SizeId { get; set; }
        public int? SizeQty { get; set; }
        public int? CutQty { get; set; }
        public bool? Finished { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
