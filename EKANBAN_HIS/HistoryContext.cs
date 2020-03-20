namespace EKANBAN_HIS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class HistoryContext : DbContext
    {
        public HistoryContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public HistoryContext(IDbName _database)
            : base(_database._SYS_HIS)
        {
        }

        public virtual DbSet<AppHistory> AppHistories { get; set; }

        public virtual DbSet<EKanbanHis> EKanbanHis { get; set; }
        public virtual DbSet<EKanbanConfirmHis> EKanbanConfirmHis { get; set; }
        public virtual DbSet<EKanbanAddHis> EKanbanAddHis { get; set; }
        public virtual DbSet<EKanbanClearHis> EKanbanClearHis { get; set; }

        public virtual DbSet<BeamCutHis> BeamCutHis { get; set; }
        public virtual DbSet<BeamCut_CuttingHis> BeamCut_CuttingHis { get; set; }
        public virtual DbSet<BeamCut_ConfirmSizeHis> BeamCut_ConfirmSizeHis { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeamCutHis>()
                .HasMany(i => i.BeamCut_CuttingHis)
                .WithOptional(i => i.BeamCutHis)
                .HasForeignKey(i => i.BeamCutHis_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BeamCut_CuttingHis>()
                .HasMany(i => i.BeamCut_ConfirmSizeHis)
                .WithOptional(i => i.BeamCut_CuttingHis)
                .HasForeignKey(i => i.BeamCut_CuttingHis_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EKanbanHis>()
                .HasMany(i => i.EKanbanAddHis)
                .WithOptional(i => i.EKanbanHis)
                .HasForeignKey(i => i.EKanbanHis_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EKanbanHis>()
                .HasMany(i => i.EKanbanConfirmHis)
                .WithOptional(i => i.EKanbanHis)
                .HasForeignKey(i => i.EKanbanHis_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EKanbanHis>()
                .HasMany(i => i.EKanbanClearHis)
                .WithOptional(i => i.EKanbanHis)
                .HasForeignKey(i => i.EKanbanHis_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
