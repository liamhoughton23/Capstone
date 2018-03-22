namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchedFunds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SchedGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GameDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SchedPractices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GameDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SchedMeets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GameDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.SubmitOffenses", "PlateAppearances", c => c.Int(nullable: false));
            AddColumn("dbo.SubmitDefenses", "Positions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmitDefenses", "Positions");
            DropColumn("dbo.SubmitOffenses", "PlateAppearances");
            DropTable("dbo.SchedMeets");
            DropTable("dbo.SchedPractices");
            DropTable("dbo.SchedGames");
            DropTable("dbo.SchedFunds");
        }
    }
}
