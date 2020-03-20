namespace EKANBAN_TASK
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class EKanbanTaskContext : DbContext
    {
        public EKanbanTaskContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public EKanbanTaskContext(IDbName _database)
            : base(_database._EKANBAN_TASK)
        {
        }

        public virtual DbSet<EKanbanDevice> EKanbanDevices { get; set; }
        public virtual DbSet<EKanbanInterface> EKanbanInterfaces { get; set; }
        public virtual DbSet<EKanbanLoading> EKanbanLoadings { get; set; }
        public virtual DbSet<EKanbanComponent> EkanbanComponents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EKanbanDevice>()
                .HasMany(i => i.EKanbanInterfaces)
                .WithOptional(i => i.EKanbanDevice)
                .HasForeignKey(k => k.EKanbanDevice_Id);

            modelBuilder.Entity<EKanbanInterface>()
                .HasMany(i => i.EkanbanComponents)
                .WithOptional(i => i.EKanbanInterface)
                .HasForeignKey(k => k.EKanbanInterface_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EKanbanInterface>()
                .HasMany(i => i.EKanbanLoadings)
                .WithOptional(i => i.EKanbanInterface)
                .HasForeignKey(k => k.EKanbanInterface_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
