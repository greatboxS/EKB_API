namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoeComponent")]
    public partial class ShoeComponent
    {
        public int Id { get; set; }

        public string Part { get; set; }

        public string Part_name_EN { get; set; }

        public string Part_name_CN { get; set; }

        public string Part_name_VN { get; set; }

        public string Reference { get; set; }
        public virtual ICollection<ModelComponent> ModelComponents { get; set; }

        [NotMapped]
        public CuttingType CuttingType { get; set; }

        [NotMapped]
        public string CuttingTypeStr
        {
            get
            {
                if (CuttingType != null) return CuttingType.TypeName;
                else return "";
            }
        }

        [NotMapped]
        public int Index { get; set; }
        [NotMapped]
        public bool AddToEkanban { get; set; }
    }
}
