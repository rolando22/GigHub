namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificoModeloInboxViewed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inboxes", "Viewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inboxes", "Viewed");
        }
    }
}
