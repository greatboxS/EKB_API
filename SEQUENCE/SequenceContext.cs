using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEQUENCE
{
    public partial class SequenceContext : DbContext
    {
        public SequenceContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public SequenceContext(IDbName _database)
            : base(_database._SEQUENCE)
        {
        }

        public virtual DbSet<OriginalPO> OriginalPOes { get; set; }
        public virtual DbSet<OriginalPOsequence> OriginalPOsequences { get; set; }
        public virtual DbSet<OriginalSize> OriginalSizes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Original table
            modelBuilder.Entity<OriginalPOsequence>()
                .HasOptional(s => s.OriginalPO)
                .WithMany(p => p.OriginalPOsequences)
                .HasForeignKey(k => k.OriginalPO_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OriginalSize>()
                .HasOptional(i => i.OriginalPOsequence)
                .WithMany(p => p.OriginalSizes)
                .HasForeignKey(k => k.OriginalPOsequence_Id)
                .WillCascadeOnDelete(true);
            // Clone table
        }
    }

}
