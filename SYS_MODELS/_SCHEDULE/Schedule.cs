namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class Schedule
    {
        public int id { get; set; }

        public string PoNumber { get; set; }

        public string Model { get; set; }

        public string ModelName { get; set; }

        public string Article { get; set; }

        public int? Quantity { get; set; }

        public string CuttingDate { get; set; }

        public string StitchindDate { get; set; }

        public int? ProductionLine_Id { get; set; }

        public int? ScheduleClass_Id { get; set; }

        public int? Building_Id { get; set; }

        //public virtual ProductionLine ProductionLine { get; set; }

        public virtual ScheduleClass ScheduleClass { get; set; }
    }
}
