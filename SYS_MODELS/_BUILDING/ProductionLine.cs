namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductionLine")]
    public partial class ProductionLine
    {
        public int id { get; set; }
        public int? Building_Id { get; set; }
        public string LineName { get; set; }
        public virtual Building Building { get; set; }
        //public virtual ICollection<OriginalPO> OriginalPOes { get; set; }
        //public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
