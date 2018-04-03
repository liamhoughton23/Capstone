namespace CAPSTONE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstuff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Calendars", "CoachID", "dbo.Coaches");
            DropIndex("dbo.Calendars", new[] { "CoachID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Calendars", "CoachID");
            AddForeignKey("dbo.Calendars", "CoachID", "dbo.Coaches", "CoachID", cascadeDelete: true);
        }
    }
}
