namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gotridofthepassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coaches", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Coaches", "Code", c => c.String(nullable: false));
        }
    }
}
