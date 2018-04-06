namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedemail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coaches", "Code", c => c.String());
            DropColumn("dbo.Coaches", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coaches", "Email", c => c.String());
            AlterColumn("dbo.Coaches", "Code", c => c.String(nullable: false));
        }
    }
}
