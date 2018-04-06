namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shits : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CodeConfirmViewModels", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CodeConfirmViewModels", "Code", c => c.String(nullable: false));
        }
    }
}
