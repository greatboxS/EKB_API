namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobTitle")]
    public partial class JobTitle
    {
        
        public int id { get; set; }

        public string Job { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}