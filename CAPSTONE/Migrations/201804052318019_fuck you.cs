namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuckyou : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CodeConfirmModels", newName: "CodeConfirmViewModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CodeConfirmViewModels", newName: "CodeConfirmModels");
        }
    }
}
