namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bigshits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LineUps", "LeadOff", c => c.String());
            AddColumn("dbo.LineUps", "LeadOffPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "SecondHitter", c => c.String());
            AddColumn("dbo.LineUps", "TwoHitPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "ThreeHitter", c => c.String());
            AddColumn("dbo.LineUps", "ThreePosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "FourHitter", c => c.String());
            AddColumn("dbo.LineUps", "Fourposition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "FiveHitter", c => c.String());
            AddColumn("dbo.LineUps", "FivePosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "SixHitter", c => c.String());
            AddColumn("dbo.LineUps", "SixPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "SevenHitter", c => c.String());
            AddColumn("dbo.LineUps", "SevenPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "EightHitter", c => c.String());
            AddColumn("dbo.LineUps", "EightPosition", c => c.Int(nullable: false));
            AddColumn("dbo.LineUps", "NineHitter", c => c.String());
            AddColumn("dbo.LineUps", "NinePosition", c => c.Int(nullable: false));
            DropColumn("dbo.LineUps", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LineUps", "Position", c => c.Int(nullable: false));
            DropColumn("dbo.LineUps", "NinePosition");
            DropColumn("dbo.LineUps", "NineHitter");
            DropColumn("dbo.LineUps", "EightPosition");
            DropColumn("dbo.LineUps", "EightHitter");
            DropColumn("dbo.LineUps", "SevenPosition");
            DropColumn("dbo.LineUps", "SevenHitter");
            DropColumn("dbo.LineUps", "SixPosition");
            DropColumn("dbo.LineUps", "SixHitter");
            DropColumn("dbo.LineUps", "FivePosition");
            DropColumn("dbo.LineUps", "FiveHitter");
            DropColumn("dbo.LineUps", "Fourposition");
            DropColumn("dbo.LineUps", "FourHitter");
            DropColumn("dbo.LineUps", "ThreePosition");
            DropColumn("dbo.LineUps", "ThreeHitter");
            DropColumn("dbo.LineUps", "TwoHitPosition");
            DropColumn("dbo.LineUps", "SecondHitter");
            DropColumn("dbo.LineUps", "LeadOffPosition");
            DropColumn("dbo.LineUps", "LeadOff");
        }
    }
}
