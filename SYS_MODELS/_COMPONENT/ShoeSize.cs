namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoeSize")]
    public partial class ShoeSize
    {
        public int id { get; set; }

        public string SizeName { get; set; }
    }
}
