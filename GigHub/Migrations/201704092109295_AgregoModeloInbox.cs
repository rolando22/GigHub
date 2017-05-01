namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoModeloInbox : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inboxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Notification_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Notification_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Notification_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inboxes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Inboxes", "Notification_Id", "dbo.Notifications");
            DropIndex("dbo.Inboxes", new[] { "User_Id" });
            DropIndex("dbo.Inboxes", new[] { "Notification_Id" });
            DropTable("dbo.Inboxes");
        }
    }
}
