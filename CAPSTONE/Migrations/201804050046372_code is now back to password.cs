namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codeisnowbacktopassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coaches", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Coaches", "Code", c => c.String());
        }
    }
}
