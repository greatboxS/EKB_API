namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Position")]
    public partial class Position
    {
        public int id { get; set; }
        public string PostionName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
