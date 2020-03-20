namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppUser")]
    public partial class AppUser
    {
        public int id { get; set; }

        public string user { get; set; }

        public string password { get; set; }

        public int? userClass { get; set; }
    }
}
