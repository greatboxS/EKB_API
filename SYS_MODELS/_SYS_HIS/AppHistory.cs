namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("AppHistory")]
    public partial class AppHistory
    {
        public int id { get; set; }
        public int? AppUser_Id { get; set; }
        public int? SysActionCode { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}

