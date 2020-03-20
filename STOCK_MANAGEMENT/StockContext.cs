namespace STOCK_MANAGEMENT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class StockContext : DbContext
    {
        public StockContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public StockContext(IDbName _database)
            : base(_database._STOCK_MANAGEMENT)
        {
        }

        public virtual DbSet<StockPreparing> StockPreparing { get; set; }
        public virtual DbSet<StockAutoCutSequence> StockAutoCutSequences { get; set; }
        public virtual DbSet<StockBeamCutSequence> StockBeamCutSequences { get; set; }
        public virtual DbSet<StockConfirmSequence> StockConfirmSequences { get; set; }
        public virtual DbSet<StockMeasure> StockMeasures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockAutoCutSequence>()
                .HasOptional(i => i.StockMeasure)
                .WithMany(i => i.StockAutoCutSequences)
                .HasForeignKey(k => k.StockMeasure_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<StockBeamCutSequence>()
                .HasOptional(i => i.StockMeasure)
                .WithMany(i => i.StockBeamCutSequences)
                .HasForeignKey(k => k.StockMeasure_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<StockConfirmSequence>()
                .HasOptional(i => i.StockMeasure)
                .WithMany(i => i.StockConfirmSequences)
                .HasForeignKey(k => k.StockMeasure_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<StockPreparing>()
                .HasOptional(i => i.StockMeasure)
                .WithMany(i => i.StockPreparing)
                .HasForeignKey(k => k.StockMeasure_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
