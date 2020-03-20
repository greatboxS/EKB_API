namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScheduleClass")]
    public partial class ScheduleClass
    {
        public int id { get; set; }

        public string Name { get; set; }

        public DateTime LastModified { get; set; }

        public int? UpdateSchProperties_Id { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual UpdateSchProperty UpdateSchProperty { get; set; }
    }
}
