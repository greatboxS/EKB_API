namespace BUILDING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuildingSchedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Building",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProductionLine",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Building_Id = c.Int(),
                        LineName = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Building", t => t.Building_Id)
                .Index(t => t.Building_Id);
            
            CreateTable(
                "dbo.ProductionLineName",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SystemCode = c.String(),
                        LineCode = c.String(),
                        DisplayCode = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionLine", "Building_Id", "dbo.Building");
            DropIndex("dbo.ProductionLine", new[] { "Building_Id" });
            DropTable("dbo.ProductionLineName");
            DropTable("dbo.ProductionLine");
            DropTable("dbo.Building");
        }
    }
}
