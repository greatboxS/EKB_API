namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FilePath")]
    public partial class FilePath
    {
        public int id { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }
    }
}
