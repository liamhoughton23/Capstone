namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtotalbasestoatable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubmitOffenses", "TotalBases", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmitOffenses", "TotalBases");
        }
    }
}
