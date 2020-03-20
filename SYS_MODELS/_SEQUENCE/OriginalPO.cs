namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OriginalPO")]
    public partial class OriginalPO
    {
        public int id { get; set; }

        public string PoNumber { get; set; }

        public int? Quantity { get; set; }

        public string Model { get; set; }

        public string Article { get; set; }

        public string ModelName { get; set; }

        public string CuttingDate { get; set; }

        public string StitchingDate { get; set; }

        public int? ProductionLine_Id { get; set; }
        //public virtual ProductionLine ProductionLine { get; set; }
        public virtual ICollection<OriginalPOsequence> OriginalPOsequences { get; set; }

        [NotMapped]
        public string Line { get; set; }
        public string UpdateTime { get; set; } = DateTime.Now.ToString("hh:mm ;dd/MM/yy");
    }
}
