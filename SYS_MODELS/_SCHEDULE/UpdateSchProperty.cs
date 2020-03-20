namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UpdateSchProperty")]
    public partial class UpdateSchProperty
    {
        public int id { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<ScheduleClass> ScheduleClasses { get; set; }
    }
}
