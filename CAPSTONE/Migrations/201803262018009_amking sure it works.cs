namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amkingsureitworks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TotalOffenses",
                c => new
                    {
                        Game = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        PlateAppearances = c.Int(nullable: false),
                        Singles = c.Int(nullable: false),
                        Doubles = c.Int(nullable: false),
                        Triples = c.Int(nullable: false),
                        HRs = c.Int(nullable: false),
                        TotalBases = c.Int(nullable: false),
                        Walks = c.Int(nullable: false),
                        HBP = c.Int(nullable: false),
                        Scrifices = c.Int(nullable: false),
                        OnByFeildersChoice = c.Int(nullable: false),
                        OnByInterference = c.Int(nullable: false),
                        DroppedThirdStrike = c.Int(nullable: false),
                        StolenBases = c.Int(nullable: false),
                        StolenBaseAttempts = c.Int(nullable: false),
                        SO = c.Int(nullable: false),
                        OtherBattingOuts = c.Int(nullable: false),
                        RBIs = c.Int(nullable: false),
                        RunsScored = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Game)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TotalOffenses", "PlayerID", "dbo.Players");
            DropIndex("dbo.TotalOffenses", new[] { "PlayerID" });
            DropTable("dbo.TotalOffenses");
        }
    }
}
