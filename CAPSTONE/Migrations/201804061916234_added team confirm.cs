namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedteamconfirm : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CodeConfirmViewModels", newName: "TeamConfirms");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TeamConfirms", newName: "CodeConfirmViewModels");
        }
    }
}
