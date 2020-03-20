namespace EMPLOYEE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUser",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user = c.String(),
                        password = c.String(),
                        userClass = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Descriptions = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        RFID_Code = c.String(),
                        UserCode = c.Int(nullable: false),
                        Name = c.String(),
                        ExperienceYears = c.Int(),
                        Address = c.String(),
                        Description = c.String(),
                        Building_Id = c.Int(),
                        Department_Id = c.Int(),
                        JobTitle_Id = c.Int(),
                        Position_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.JobTitle", t => t.JobTitle_Id)
                .ForeignKey("dbo.Position", t => t.Position_Id)
                .ForeignKey("dbo.Department", t => t.Department_Id)
                .Index(t => t.Department_Id)
                .Index(t => t.JobTitle_Id)
                .Index(t => t.Position_Id);
            
            CreateTable(
                "dbo.JobTitle",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Job = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PostionName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "Department_Id", "dbo.Department");
            DropForeignKey("dbo.Employee", "Position_Id", "dbo.Position");
            DropForeignKey("dbo.Employee", "JobTitle_Id", "dbo.JobTitle");
            DropIndex("dbo.Employee", new[] { "Position_Id" });
            DropIndex("dbo.Employee", new[] { "JobTitle_Id" });
            DropIndex("dbo.Employee", new[] { "Department_Id" });
            DropTable("dbo.Position");
            DropTable("dbo.JobTitle");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
            DropTable("dbo.AppUser");
        }
    }
}
