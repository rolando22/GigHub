namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificoModeloGigValidaciones1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gigs", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Gigs", new[] { "Genre_Id" });
            RenameColumn(table: "dbo.Gigs", name: "Genre_Id", newName: "GenreId");
            AlterColumn("dbo.Gigs", "GenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Gigs", "GenreId");
            AddForeignKey("dbo.Gigs", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "GenreId", "dbo.Genres");
            DropIndex("dbo.Gigs", new[] { "GenreId" });
            AlterColumn("dbo.Gigs", "GenreId", c => c.Int());
            RenameColumn(table: "dbo.Gigs", name: "GenreId", newName: "Genre_Id");
            CreateIndex("dbo.Gigs", "Genre_Id");
            AddForeignKey("dbo.Gigs", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}
