namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
  
    [Table("BDeviceCutTimeRecord")]
    public class BDeviceCutTimeRecord
    {
        public int id { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("hh: mm:ss tt, dd / MM/yyyy");
        public DateTime? UpdateTime { get; set; } = DateTime.Now;
        public int? TotalCutTime { get; set; }
        public int? ConfirmQuantity { get; set; }
        public virtual int? BeamCutDevice_Id{ get; set; }
        public virtual BeamCutDevice BeamCutDevice { get; set; }
        public virtual BMachineStatistic BMachineStatistic { get; set; }
        public int? BMachineStatistic_Id { get; set; }
    }
}
