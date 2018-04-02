namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlocationtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        Destination = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                    })
                .PrimaryKey(t => t.LocationID);
            
            DropTable("dbo.LocationGetters");
        }
        
        public override void Down()
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
            
            DropTable("dbo.Locations");
        }
    }
}
