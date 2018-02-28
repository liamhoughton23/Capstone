namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedabunchoftables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GamePlays",
                c => new
                    {
                        Inning = c.Int(nullable: false, identity: true),
                        Outs = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Inning);
            
            DropTable("dbo.PossibleOutcomes");
        }
        
        public override void Down()
        {
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
            
            DropTable("dbo.GamePlays");
        }
    }
}
