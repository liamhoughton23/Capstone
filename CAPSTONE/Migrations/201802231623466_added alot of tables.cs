namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedalotoftables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        CoachID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.CoachID);
            
            CreateTable(
                "dbo.DefenseStats",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        Positions = c.Int(nullable: false),
                        Games = c.Int(nullable: false),
                        IP = c.Int(nullable: false),
                        TC = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        Errors = c.Int(nullable: false),
                        DoublePlays = c.Int(nullable: false),
                        FPCT = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CoachID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.Coaches", t => t.CoachID, cascadeDelete: true)
                .Index(t => t.CoachID);
            
            CreateTable(
                "dbo.OffenseStats",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Player = c.Int(nullable: false),
                        AtBats = c.Int(nullable: false),
                        Runs = c.Int(nullable: false),
                        Hits = c.Int(nullable: false),
                        FirstBase = c.Int(nullable: false),
                        SecondBase = c.Int(nullable: false),
                        ThirdBase = c.Int(nullable: false),
                        HR = c.Int(nullable: false),
                        RBI = c.Int(nullable: false),
                        Walks = c.Int(nullable: false),
                        StrikeOuts = c.Int(nullable: false),
                        StolenBases = c.Int(nullable: false),
                        CaughtStolenBases = c.Int(nullable: false),
                        BA = c.Single(nullable: false),
                        OBP = c.Single(nullable: false),
                        SLG = c.Single(nullable: false),
                        OPS = c.Single(nullable: false),
                        RC = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Players", t => t.Player, cascadeDelete: true)
                .Index(t => t.Player);
            
            CreateTable(
                "dbo.PossibleOutcomes",
                c => new
                    {
                        Play = c.Int(nullable: false, identity: true),
                        Hit = c.Int(nullable: false),
                        Single = c.Int(nullable: false),
                        Double = c.Int(nullable: false),
                        Triple = c.Int(nullable: false),
                        HR = c.Int(nullable: false),
                        BB = c.Int(nullable: false),
                        HBP = c.Int(nullable: false),
                        FC = c.Int(nullable: false),
                        WildPitch = c.Int(nullable: false),
                        CatchersInterference = c.Int(nullable: false),
                        PassedBall = c.Int(nullable: false),
                        GRD = c.Int(nullable: false),
                        Error = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Play);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OffenseStats", "Player", "dbo.Players");
            DropForeignKey("dbo.DefenseStats", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.Players", "CoachID", "dbo.Coaches");
            DropIndex("dbo.OffenseStats", new[] { "Player" });
            DropIndex("dbo.Players", new[] { "CoachID" });
            DropIndex("dbo.DefenseStats", new[] { "PlayerID" });
            DropTable("dbo.PossibleOutcomes");
            DropTable("dbo.OffenseStats");
            DropTable("dbo.Players");
            DropTable("dbo.DefenseStats");
            DropTable("dbo.Coaches");
        }
    }
}
