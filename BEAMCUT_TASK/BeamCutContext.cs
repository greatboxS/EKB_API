namespace BEAMCUT_TASK
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class BeamCutContext : DbContext
    {
        public BeamCutContext(string _connectionString)
           : base(_connectionString)
        {
        }
        public BeamCutContext(IDbName _database)
                    : base(_database._BEAMCUT_TASK)
        {
        }

        public virtual DbSet<BeamCutDevice> BeamCutDevices { get; set; }
        public virtual DbSet<BeamCutInterface> BeamCutInterfaces { get; set; }
        public virtual DbSet<BeamCutSize> BeamCutSizes { get; set; }
        public virtual DbSet<BeamCutPo> BeamCutPos { get; set; }
        public virtual DbSet<BeamCutSeq> BeamCutSeqs { get; set; }

        public virtual DbSet<BDeviceCutTimeRecord> BDeviceCutTimeRecords { get; set; }
        public virtual DbSet<BDeviceOrder> BDeviceOrders { get; set; }
        public virtual DbSet<BDeviceOrderComponent> BDeviceOrderComponents { get; set; }
        public virtual DbSet<BMachineStatistic> BMachineStatistices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeamCutDevice>()
                .HasMany(e => e.BeamCutInterfaces)
                .WithOptional(e => e.BeamCutDevice)
                .HasForeignKey(e => e.BeamCutDevice_Id);

            modelBuilder.Entity<BeamCutSeq>()
                .HasMany(e => e.BeamCutSizes)
                .WithOptional(e => e.BeamCutSeq)
                .HasForeignKey(e => e.BeamCutSeq_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BeamCutPo>()
                .HasMany(e => e.BeamCutSeqs)
                .WithOptional(e => e.BeamCutPo)
                .HasForeignKey(e => e.BeamCutPo_Id)
                 .WillCascadeOnDelete(true);

            modelBuilder.Entity<BeamCutPo>()
                .HasMany(e => e.BeamCutInterfaces)
                .WithOptional(e => e.BeamCutPo)
                .HasForeignKey(e => e.BeamCutPo_Id);

            modelBuilder.Entity<BeamCutDevice>()
               .HasMany(i => i.BeamCutSeqs)
               .WithOptional(i => i.BeamCutDevice)
               .HasForeignKey(k => k.BeamCutDevice_Id);

            modelBuilder.Entity<BeamCutDevice>()
               .HasMany(i => i.BeamCutSizes)
               .WithOptional(i => i.BeamCutDevice)
               .HasForeignKey(k => k.BeamCutDevice_Id);

            modelBuilder.Entity<BeamCutDevice>()
                .HasMany(i => i.BDeviceOrders)
                .WithOptional(i => i.BeamCutDevice)
                .HasForeignKey(k => k.BeamCutDevice_Id);

            modelBuilder.Entity<BeamCutDevice>()
                .HasMany(i => i.BDeviceCutTimeRecords)
                .WithOptional(i => i.BeamCutDevice)
                .HasForeignKey(k => k.BeamCutDevice_Id);

            modelBuilder.Entity<BeamCutDevice>()
               .HasMany(i => i.BMachineStatistices)
               .WithOptional(i => i.BeamCutDevice)
               .HasForeignKey(k => k.BeamCutDevice_Id);

            modelBuilder.Entity<BDeviceOrderComponent>()
                .HasOptional(i => i.BDeviceOrder)
                .WithMany(i => i.BDeviceOrderComponents)
                .HasForeignKey(k => k.BDeviceOrder_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BMachineStatistic>()
                .HasMany(i => i.BDeviceCutTimeRecords)
                .WithOptional(i => i.BMachineStatistic)
                .HasForeignKey(k => k.BMachineStatistic_Id);
        }
    }
}
