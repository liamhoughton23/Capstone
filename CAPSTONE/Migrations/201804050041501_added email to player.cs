namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedemailtoplayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Email");
        }
    }
}
