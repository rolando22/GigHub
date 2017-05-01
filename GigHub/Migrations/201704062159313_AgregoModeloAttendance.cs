namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoModeloAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Artist_Id = c.String(maxLength: 128),
                        Gig_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.Gigs", t => t.Gig_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.Gig_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Gig_Id", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "Gig_Id" });
            DropIndex("dbo.Attendances", new[] { "Artist_Id" });
            DropTable("dbo.Attendances");
        }
    }
}
