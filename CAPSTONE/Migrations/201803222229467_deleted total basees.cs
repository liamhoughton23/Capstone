namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedtotalbasees : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OffenseStats", "TotalBases");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OffenseStats", "TotalBases", c => c.Single(nullable: false));
        }
    }
}
