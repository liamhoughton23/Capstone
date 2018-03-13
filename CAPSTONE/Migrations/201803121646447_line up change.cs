namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lineupchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LineUps", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "PlayerId", c => c.Int(nullable: false));
            CreateIndex("dbo.LineUps", "PlayerId");
            AddForeignKey("dbo.LineUps", "PlayerId", "dbo.Players", "PlayerID", cascadeDelete: true);
            DropColumn("dbo.LineUps", "LeadOff");
            DropColumn("dbo.LineUps", "LeadOffPosition");
            DropColumn("dbo.LineUps", "SecondHitter");
            DropColumn("dbo.LineUps", "TwoHitPosition");
            DropColumn("dbo.LineUps", "ThreeHitter");
            DropColumn("dbo.LineUps", "ThreePosition");
            DropColumn("dbo.LineUps", "FourHitter");
            DropColumn("dbo.LineUps", "Fourposition");
            DropColumn("dbo.LineUps", "FiveHitter");
            DropColumn("dbo.LineUps", "FivePosition");
            DropColumn("dbo.LineUps", "SixHitter");
            DropColumn("dbo.LineUps", "SixPosition");
            DropColumn("dbo.LineUps", "SevenHitter");
            DropColumn("dbo.LineUps", "SevenPosition");
            DropColumn("dbo.LineUps", "EightHitter");
            DropColumn("dbo.LineUps", "EightPosition");
            DropColumn("dbo.LineUps", "NineHitter");
            DropColumn("dbo.LineUps", "NinePosition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LineUps", "NinePosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "NineHitter", c => c.String());
            AddColumn("dbo.LineUps", "EightPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "EightHitter", c => c.String());
            AddColumn("dbo.LineUps", "SevenPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "SevenHitter", c => c.String());
            AddColumn("dbo.LineUps", "SixPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "SixHitter", c => c.String());
            AddColumn("dbo.LineUps", "FivePosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "FiveHitter", c => c.String());
            AddColumn("dbo.LineUps", "Fourposition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "FourHitter", c => c.String());
            AddColumn("dbo.LineUps", "ThreePosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "ThreeHitter", c => c.String());
            AddColumn("dbo.LineUps", "TwoHitPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "SecondHitter", c => c.String());
            AddColumn("dbo.LineUps", "LeadOffPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "LeadOff", c => c.String());
            DropForeignKey("dbo.LineUps", "PlayerId", "dbo.Players");
            DropIndex("dbo.LineUps", new[] { "PlayerId" });
            DropColumn("dbo.LineUps", "PlayerId");
            DropColumn("dbo.LineUps", "Position");
        }
    }
}
