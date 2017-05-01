namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificoModeloNotificationGigId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Gig", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Gig");
        }
    }
}
