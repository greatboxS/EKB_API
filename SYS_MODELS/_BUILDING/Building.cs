namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Building")]
    public partial class Building
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public virtual ICollection<ProductionLine> ProductionLines { get; set; }
    }
}
