namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("BeamCutInterface")]
    public partial class BeamCutInterface
    {
        public int id { get; set; }
        public int? Employee_Id { get; set; }
        public int? BeamCutDevice_Id { get; set; }
        public int? BDeviceOrder_Id { get; set; }
        //public BDeviceOrder BDeviceOrder { get; set; }
        public virtual BeamCutDevice BeamCutDevice { get; set; }
        public int? OriginalPo_Id { get; set; }
        public int? Schedule_Id { get; set; }
        public int? BeamCutPo_Id { get; set; }
        public virtual BeamCutPo BeamCutPo { get; set; }
        public int? BeamCutCounter { get; set; }
        public int? CuttingQty { get; set; }
        public int? BeamCutStartSeq_Id { get; set; }
        public int? BeamCutStopSeq_Id { get; set; }
        public int? TotalSelectedQty { get; set; }
        public int? TotalSelectCutQty { get; set; }
        public int? StartSeqNo { get; set; }
        public int? StopSeqNo { get; set; }
        public bool? Finish { get; set; }
        public DateTime? StartCutTime { get; set; }
        public DateTime? StopCutTime { get; set; }
        public DateTime? LastConfirmSize { get; set; }

    }
}
