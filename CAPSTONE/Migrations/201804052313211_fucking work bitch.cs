namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuckingworkbitch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodeConfirmModels",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CodeConfirmModels");
        }
    }
}
