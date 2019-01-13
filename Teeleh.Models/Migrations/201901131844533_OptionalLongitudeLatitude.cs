namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalLongitudeLatitude : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "Latitude", c => c.Double());
            AlterColumn("dbo.Advertisements", "Longitude", c => c.Double());
            AlterColumn("dbo.Requests", "MinPrice", c => c.Single());
            AlterColumn("dbo.Requests", "MaxPrice", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "MaxPrice", c => c.Single(nullable: false));
            AlterColumn("dbo.Requests", "MinPrice", c => c.Single(nullable: false));
            AlterColumn("dbo.Advertisements", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Advertisements", "Latitude", c => c.Double(nullable: false));
        }
    }
}
