namespace EMPLOYEE
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SYS_MODELS;

    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext(string _connectionString)
            : base(_connectionString)
        {
        }

        public EmployeeContext(IDbName _database)
            : base(_database._EMPLOYEE)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.Department_Id);

            modelBuilder.Entity<JobTitle>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.JobTitle)
                .HasForeignKey(e => e.JobTitle_Id);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Position)
                .HasForeignKey(e => e.Position_Id);
        }
    }
}
