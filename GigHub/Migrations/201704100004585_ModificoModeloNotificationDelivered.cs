namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificoModeloNotificationDelivered : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Delivered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Delivered");
        }
    }
}
