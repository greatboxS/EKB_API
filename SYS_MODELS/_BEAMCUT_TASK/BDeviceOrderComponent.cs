using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS_MODELS
{
    [Table("BDeviceOrderComponent")]
    public class BDeviceOrderComponent
    {
        public int id { get; set; }
        public int? BDeviceOrder_Id { get; set; }
        public virtual BDeviceOrder BDeviceOrder { get; set; }
        public int? Schedule_id { get; set; }
    }
}
