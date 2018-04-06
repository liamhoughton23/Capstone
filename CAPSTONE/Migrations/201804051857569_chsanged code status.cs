namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chsangedcodestatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "Code", c => c.String(nullable: false));
        }
    }
}
