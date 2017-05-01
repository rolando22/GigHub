namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PoblarGenres : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO Genres (Name) VALUES ('Rock and Roll')");
            Sql(@"INSERT INTO Genres (Name) VALUES ('Blues')");
            Sql(@"INSERT INTO Genres (Name) VALUES ('Jazz')");
            Sql(@"INSERT INTO Genres (Name) VALUES ('Pop')");
            Sql(@"INSERT INTO Genres (Name) VALUES ('Punk')");
            Sql(@"INSERT INTO Genres (Name) VALUES ('Hip Hop')");
            Sql(@"INSERT INTO Genres (Name) VALUES ('Rap')");
        }
        
        public override void Down()
        {
        }
    }
}
