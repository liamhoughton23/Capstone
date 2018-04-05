namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedshit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Code", c => c.String(nullable: false));
        }
    }
}
