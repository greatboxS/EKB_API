namespace COMPONENT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class ShoeContext : DbContext
    {
        public ShoeContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public ShoeContext(IDbName _database)
            : base(_database._COMPONENT)
        {
        }

        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<ModelComponent> ModelComponents { get; set; }
        public virtual DbSet<ShoeArticle> ShoeArticles { get; set; }
        public virtual DbSet<ShoeComponent> ShoeComponents { get; set; }
        public virtual DbSet<ShoeModel> ShoeModels { get; set; }
        public virtual DbSet<ShoeSize> ShoeSizes { get; set; }
        public virtual DbSet<CuttingType> CuttingTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>()
                .HasMany(e => e.ModelComponents)
                .WithOptional(e => e.Material)
                .HasForeignKey(e => e.Material_Id);

            modelBuilder.Entity<ShoeComponent>()
                .HasMany(e => e.ModelComponents)
                .WithOptional(e => e.ShoeComponent)
                .HasForeignKey(e => e.ShoeComponent_Id);

            modelBuilder.Entity<ShoeModel>()
                .HasMany(e => e.ModelComponents)
                .WithOptional(e => e.ShoeModel)
                .HasForeignKey(e => e.ShoeModel_Id);

            modelBuilder.Entity<ShoeArticle>()
                .HasMany(e => e.ModelComponents)
                .WithOptional(e => e.ShoeArticle)
                .HasForeignKey(e => e.ShoeArticle_Id);

            modelBuilder.Entity<CuttingType>()
                .HasMany(e => e.ModelComponents)
                .WithOptional(e => e.CuttingType)
                .HasForeignKey(e => e.CuttingType_Id);
        }
    }
}
