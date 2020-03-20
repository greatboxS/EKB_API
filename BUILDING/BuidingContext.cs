namespace BUILDING
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class BuildingContext : DbContext
    {
        public BuildingContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public BuildingContext(IDbName _database)
            : base(_database._BUILDING)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<ProductionLine> ProductionLines { get; set; }
        public virtual DbSet<ProductionLineName> ProductionLineNames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>()
                .HasMany(e => e.ProductionLines)
                .WithOptional(e => e.Building)
                .HasForeignKey(e => e.Building_Id);
        }
    }
}
