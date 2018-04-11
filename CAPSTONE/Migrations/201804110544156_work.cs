namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class work : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactPlayers",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        PlayerID = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactPlayers", "PlayerID", "dbo.Players");
            DropIndex("dbo.ContactPlayers", new[] { "PlayerID" });
            DropTable("dbo.ContactPlayers");
        }
    }
}
