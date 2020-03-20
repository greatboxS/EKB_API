namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductionLineName")]
    public partial class ProductionLineName
    {
        public int id { get; set; }

        public string SystemCode { get; set; }

        public string LineCode { get; set; }

        public string DisplayCode { get; set; }
    }
}
