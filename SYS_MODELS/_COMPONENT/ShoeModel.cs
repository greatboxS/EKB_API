namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoeModel")]
    public partial class ShoeModel
    {
        public int Id { get; set; }

        public string Model_Number { get; set; }

        public string Model_Name { get; set; }

        public virtual ICollection<ModelComponent> ModelComponents { get; set; }

        public virtual ICollection<ShoeArticle> ShoeArticles { get; set; }
    }
}
