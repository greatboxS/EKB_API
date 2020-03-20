namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("CuttingType")]
    public class CuttingType
    {
        public int id { get; set; }
        public string  TypeName { get; set; }
        public virtual ICollection<ModelComponent> ModelComponents { get; set; }
    }
}
