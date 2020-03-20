namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoeArticle")]
    public partial class ShoeArticle
    {
        public int id { get; set; }

        public string Article_Name { get; set; }

        public string Code { get; set; }

        public virtual ICollection<ModelComponent> ModelComponents { get; set; }
    }
}
