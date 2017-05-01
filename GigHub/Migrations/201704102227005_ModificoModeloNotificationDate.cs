namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificoModeloNotificationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "Information", c => c.String());
            AddColumn("dbo.Notifications", "GigDate", c => c.String());
            DropColumn("dbo.Notifications", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Text", c => c.String());
            DropColumn("dbo.Notifications", "GigDate");
            DropColumn("dbo.Notifications", "Information");
            DropColumn("dbo.Notifications", "Date");
        }
    }
}
