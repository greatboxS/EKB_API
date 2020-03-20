namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModelComponent")]
    public partial class ModelComponent
    {
        public int id { get; set; }

        public int? ShoeModel_Id { get; set; }

        public int? ShoeComponent_Id { get; set; }

        public int? Material_Id { get; set; }

        public int? CuttingType_Id { get; set; }

        public int? ShoeArticle_Id { get; set; }

        public virtual CuttingType CuttingType { get; set; }

        public virtual ShoeArticle ShoeArticle { get; set; }

        public virtual Material Material { get; set; }

        public virtual ShoeComponent ShoeComponent { get; set; }

        public virtual ShoeModel ShoeModel { get; set; }
    }
}
