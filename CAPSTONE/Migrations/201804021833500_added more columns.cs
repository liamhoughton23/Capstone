namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmorecolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Locations", c => c.String());
            AddColumn("dbo.Locations", "Latitude2", c => c.String());
            AddColumn("dbo.Locations", "Longitude2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Longitude2");
            DropColumn("dbo.Locations", "Latitude2");
            DropColumn("dbo.Locations", "Locations");
        }
    }
}
