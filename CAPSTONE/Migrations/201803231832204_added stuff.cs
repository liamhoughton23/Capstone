namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OffenseStats", "TotalBases", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OffenseStats", "TotalBases");
        }
    }
}
