namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocationGetters",
                c => new
                    {
                        DestinationID = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Destination = c.String(),
                    })
                .PrimaryKey(t => t.DestinationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LocationGetters");
        }
    }
}
