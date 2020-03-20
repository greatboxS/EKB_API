namespace SYS_MODELS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BeamCutHis")]
    public partial class BeamCutHis
    {
        public int id { get; set; }

        public int? BeamMachine_Id { get; set; }

        public int? Employee_Id { get; set; }

        public int? BeamCutInterface_Id { get; set; }

        public virtual ICollection<BeamCut_CuttingHis> BeamCut_CuttingHis { get; set; }
        

    }
}
