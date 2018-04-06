namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Member", c => c.Boolean(nullable: false));
            AddColumn("dbo.Players", "Member", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Member");
            DropColumn("dbo.AspNetUsers", "Member");
        }
    }
}
