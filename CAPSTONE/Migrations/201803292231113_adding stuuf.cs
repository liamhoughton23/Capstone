namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingstuuf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calendars",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Color = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventID);
            
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
                        Position = c.Int(nullable: false),
                        IP = c.Int(nullable: false),
                        TC = c.Int(nullable: false),
                        PO = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        Errors = c.Int(nullable: false),
                        FPCT = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            
            CreateTable(
                "dbo.LineUps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.OffenseStats",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        TotalPlateAppearances = c.Int(nullable: false),
                        OfficialAtBats = c.Int(nullable: false),
                        TotalHits = c.Int(nullable: false),
                        TotalBases = c.Int(nullable: false),
                        BA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SLG = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OBP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BOBP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SBP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RunsCreated = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.PitchStats",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        EarnedRunAvereage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpponentBattingAverage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WHIP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StrikeOuts = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StrikeOutPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PickOffPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WalksPerAtBat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WalksPerInning = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SubmitDefenses",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        Positions = c.Int(nullable: false),
                        Errors = c.Int(nullable: false),
                        InningsPlayed = c.Int(nullable: false),
                        PutOuts = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.SubmitOffenses",
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
            
            CreateTable(
                "dbo.SubmitPitchings",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        OpponentOfficialAtBats = c.Int(nullable: false),
                        OpponentHits = c.Int(nullable: false),
                        EarnedRunsScored = c.Int(nullable: false),
                        InningsPitched = c.Int(nullable: false),
                        StrikeOuts = c.Int(nullable: false),
                        HomeRuns = c.Int(nullable: false),
                        Walks = c.Int(nullable: false),
                        BattersHBP = c.Int(nullable: false),
                        PickOffAttempts = c.Int(nullable: false),
                        PickOffs = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
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
            
            CreateTable(
                "dbo.TotalPitchings",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        OpponentOfficialAtBats = c.Int(nullable: false),
                        OpponentHits = c.Int(nullable: false),
                        EarnedRunsScored = c.Int(nullable: false),
                        InningsPitched = c.Int(nullable: false),
                        StrikeOuts = c.Int(nullable: false),
                        HomeRuns = c.Int(nullable: false),
                        Walks = c.Int(nullable: false),
                        BattersHBP = c.Int(nullable: false),
                        PickOffAttempts = c.Int(nullable: false),
                        PickOffs = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TotalPitchings", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.TotalDefenses", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.SubmitPitchings", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.SubmitOffenses", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.SubmitDefenses", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PitchStats", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.OffenseStats", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.LineUps", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.TotalOffenses", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.DefenseStats", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.Players", "CoachID", "dbo.Coaches");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TotalPitchings", new[] { "PlayerID" });
            DropIndex("dbo.TotalDefenses", new[] { "PlayerID" });
            DropIndex("dbo.SubmitPitchings", new[] { "PlayerID" });
            DropIndex("dbo.SubmitOffenses", new[] { "PlayerID" });
            DropIndex("dbo.SubmitDefenses", new[] { "PlayerID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PitchStats", new[] { "PlayerID" });
            DropIndex("dbo.OffenseStats", new[] { "PlayerID" });
            DropIndex("dbo.LineUps", new[] { "PlayerID" });
            DropIndex("dbo.TotalOffenses", new[] { "PlayerID" });
            DropIndex("dbo.Players", new[] { "CoachID" });
            DropIndex("dbo.DefenseStats", new[] { "PlayerID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TotalPitchings");
            DropTable("dbo.TotalDefenses");
            DropTable("dbo.SubmitPitchings");
            DropTable("dbo.SubmitOffenses");
            DropTable("dbo.SubmitDefenses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PitchStats");
            DropTable("dbo.OffenseStats");
            DropTable("dbo.LineUps");
            DropTable("dbo.TotalOffenses");
            DropTable("dbo.Players");
            DropTable("dbo.DefenseStats");
            DropTable("dbo.Coaches");
            DropTable("dbo.Calendars");
        }
    }
}
