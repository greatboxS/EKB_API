namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int id { get; set; }

        public string RFID_Code { get; set; }

        public int UserCode { get; set; }

        public string Name { get; set; }

        public int? ExperienceYears { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public int? Building_Id { get; set; }

        public int? Department_Id { get; set; }

        public int? JobTitle_Id { get; set; }

        public int? Position_Id { get; set; }

        public virtual Position Position { get; set; }

        public virtual Department Department { get; set; }

        public virtual JobTitle JobTitle { get; set; }
    }
}
