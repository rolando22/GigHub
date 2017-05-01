namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoModeloFollowUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Followed_Id = c.String(maxLength: 128),
                        Follower_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Followed_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Follower_Id)
                .Index(t => t.Followed_Id)
                .Index(t => t.Follower_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowUps", "Follower_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowUps", "Followed_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FollowUps", new[] { "Follower_Id" });
            DropIndex("dbo.FollowUps", new[] { "Followed_Id" });
            DropTable("dbo.FollowUps");
        }
    }
}
