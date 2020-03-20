namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OriginalSize")]
    public partial class OriginalSize
    {
        public int id { get; set; }

        public int? SizeId { get; set; }

        public int? Quantity { get; set; }

        public int? OriginalPOsequence_Id { get; set; }

        public virtual OriginalPOsequence OriginalPOsequence { get; set; }
    }
}
