namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificoModeloGigValidaciones : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gigs", "Venue", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Gigs", "Venue", c => c.String());
        }
    }
}
