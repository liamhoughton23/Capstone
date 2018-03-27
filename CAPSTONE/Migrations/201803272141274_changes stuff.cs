namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesstuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TotalDefenses",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        Positions = c.Int(nullable: false),
                        Attempts = c.Int(nullable: false),
                        Errors = c.Int(nullable: false),
                        InningsPlayed = c.Int(nullable: false),
                        PutOuts = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            DropColumn("dbo.DefenseStats", "Games");
            DropColumn("dbo.DefenseStats", "DoublePlays");
            DropColumn("dbo.SubmitDefenses", "Attempts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubmitDefenses", "Attempts", c => c.Int(nullable: false));
            AddColumn("dbo.DefenseStats", "DoublePlays", c => c.Int(nullable: false));
            AddColumn("dbo.DefenseStats", "Games", c => c.Int(nullable: false));
            DropForeignKey("dbo.TotalDefenses", "PlayerID", "dbo.Players");
            DropIndex("dbo.TotalDefenses", new[] { "PlayerID" });
            DropTable("dbo.TotalDefenses");
        }
    }
}
