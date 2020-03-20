namespace SCHEDULE
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class ScheduleContext : DbContext
    {
        public ScheduleContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public ScheduleContext()
            : base(DbConnectionString._SCHEDULE)
        {
        }

        public ScheduleContext(IDbName _database)
            : base(_database._SCHEDULE)
        {
        }

        public virtual DbSet<FilePath> FilePaths { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleClass> ScheduleClasses { get; set; }
        public virtual DbSet<UpdateSchProperty> UpdateSchProperties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleClass>()
                .HasMany(e => e.Schedules)
                .WithOptional(e => e.ScheduleClass)
                .HasForeignKey(e => e.ScheduleClass_Id);

            modelBuilder.Entity<UpdateSchProperty>()
                .HasMany(e => e.ScheduleClasses)
                .WithOptional(e => e.UpdateSchProperty)
                .HasForeignKey(e => e.UpdateSchProperties_Id);
        }
    }
}
