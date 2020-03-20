namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("BeamCutDevice")]
    public partial class BeamCutDevice
    {
        public int id { get; set; }
        public string MachineCode { get; set; }
        public string Name { get; set; }
        public int? Building_Id { get; set; }
        public int? NetworkStatus { get; set; }
        public DateTime? LastOnline { get; set; }
        public virtual ICollection<BeamCutInterface> BeamCutInterfaces { get; set; }
        public virtual ICollection<BDeviceOrder> BDeviceOrders { get; set; }
        public virtual ICollection<BeamCutSeq> BeamCutSeqs { get; set; }
        public virtual ICollection<BeamCutSize> BeamCutSizes { get; set; }
        public virtual ICollection<BDeviceCutTimeRecord> BDeviceCutTimeRecords { get; set; }
        public virtual ICollection<BMachineStatistic> BMachineStatistices { get; set; }

    }
}
