namespace EMPLOYEE.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SYS_MODELS;

    internal sealed class Configuration : DbMigrationsConfiguration<EMPLOYEE.EmployeeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EMPLOYEE.EmployeeContext context)
        {
            context.Positions.AddRange(new Position[] {
                new Position { PostionName = "Division Supervisor" },
                new Position { PostionName = "Building Supervisor" },
                new Position { PostionName = "Beam Worker" },
                new Position { PostionName = "Auto Worker" },
                new Position { PostionName = "Water Spider" }}
            );

            context.AppUsers.AddRange(new AppUser[] {
                new AppUser { user = "admin", password="admin", userClass=3 },
                new AppUser { user = "officer", password="officer", userClass=2 },
                new AppUser { user = "worker", password="123", userClass=1 }}
            );

            context.JobTitles.AddRange(new JobTitle[]
            {
                new JobTitle{ Job= "Chặt phụ kiện"},
                new JobTitle{ Job= "Chặt da"},
            });

            context.SaveChanges();

            context.Departments.AddRange(new Department[]
            {
                new Department{Code="A8L",},
                 new Department{Code="A8J",},
            });
            context.SaveChanges();


            context.Employees.AddRange(new Employee[]
            {
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Nguyễn Thị Ngọc Hương",
                    UserCode = 48798,
                    RFID_Code = "29CF654F",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Đặng Khắc Ghi",
                    UserCode = 49722,
                    RFID_Code = "0C31E1CB",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Lê Thị Kim Loan",
                    UserCode = 41270,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Đinh Thị Liên",
                    UserCode = 39385,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Đồng Thị Thùy",
                    UserCode = 48097,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Hồ Thị Ánh Nga ",
                    UserCode = 50598,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Trần Kim Lý",
                    UserCode = 52211,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
            });
            context.SaveChanges();
        }
    }
}
