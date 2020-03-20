namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Material")]
    public partial class Material
    {
        public int id { get; set; }

        public string Material_Number { get; set; }

        public string Material_Description { get; set; }

        public string Unit { get; set; }

        public virtual ICollection<ModelComponent> ModelComponents { get; set; }
    }
}
