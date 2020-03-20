namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("BeamCut_ConfirmSizeHis")]
    public partial class BeamCut_ConfirmSizeHis
    {
        public int id { get; set; }
        public DateTime? DateTime { get; set; }
        public int? SizeId { get; set; }
        public int? Qty { get; set; }
        public  virtual BeamCut_CuttingHis BeamCut_CuttingHis { get; set; }
        public int? BeamCut_CuttingHis_Id { get; set; }
    }
}
