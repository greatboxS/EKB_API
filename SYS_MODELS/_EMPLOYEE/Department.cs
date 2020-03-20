namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public int id { get; set; }

        public string Code { get; set; }

        public string Descriptions { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
