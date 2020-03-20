using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS_MODELS
{
    public class BMachineStatistic
    {
        public int id { get; set; }
        public int? BeamCutDevice_Id { get; set; }
        public int? TotalCuttime { get; set; }
        public int? TotalCutQty { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public virtual ICollection<BDeviceCutTimeRecord> BDeviceCutTimeRecords { get; set; }
        public virtual BeamCutDevice BeamCutDevice { get; set; }
        public DateTime? StartTime { get; set; } = DateTime.Now;
        public DateTime? StopTime { get; set; }
    }
}
