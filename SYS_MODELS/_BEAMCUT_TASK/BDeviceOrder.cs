using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS_MODELS
{
    [Table("BDeviceOrder")]
    public class BDeviceOrder
    {
        public int id { get; set; }
        public int? Place  { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public DateTime? OrderDate { get; set; }
        public int? BeamCutDevice_Id { get; set; }
        public virtual BeamCutDevice BeamCutDevice { get; set; }
        public string PoNumber { get; set; }
        public int? PoQty { get; set; }
        public int? ProductionLine_Id { get; set; }
        public int? Schedule_Id { get; set; }
        public int? ScheduleClass_Id { get; set; }
        public int? AddPeople_Id { get; set; }
        public int? WorkerId { get; set; }
        public virtual List<BDeviceOrderComponent> BDeviceOrderComponents { get; set; }
        //public virtual List<BeamCutInterface> BeamCutInterfaces { get; set; }
    }
}
