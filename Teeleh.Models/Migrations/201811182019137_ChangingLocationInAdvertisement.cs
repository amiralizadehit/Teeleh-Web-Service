namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingLocationInAdvertisement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "LocationId", "dbo.Locations");
            DropIndex("dbo.Advertisements", new[] { "LocationId" });
            AddColumn("dbo.Advertisements", "GameReg", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "LocationRegionId", c => c.Int());
            AddColumn("dbo.Advertisements", "LocationCityId", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "LocationProvinceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Advertisements", "LocationCityId");
            CreateIndex("dbo.Advertisements", "LocationProvinceId");
            CreateIndex("dbo.Advertisements", "LocationRegionId");
            AddForeignKey("dbo.Advertisements", "LocationCityId", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Advertisements", "LocationProvinceId", "dbo.Locations", "Id");
            AddForeignKey("dbo.Advertisements", "LocationRegionId", "dbo.Locations", "Id");
            DropColumn("dbo.Advertisements", "LocationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "LocationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Advertisements", "LocationRegionId", "dbo.Locations");
            DropForeignKey("dbo.Advertisements", "LocationProvinceId", "dbo.Locations");
            DropForeignKey("dbo.Advertisements", "LocationCityId", "dbo.Locations");
            DropIndex("dbo.Advertisements", new[] { "LocationRegionId" });
            DropIndex("dbo.Advertisements", new[] { "LocationProvinceId" });
            DropIndex("dbo.Advertisements", new[] { "LocationCityId" });
            DropColumn("dbo.Advertisements", "LocationProvinceId");
            DropColumn("dbo.Advertisements", "LocationCityId");
            DropColumn("dbo.Advertisements", "LocationRegionId");
            DropColumn("dbo.Advertisements", "GameReg");
            CreateIndex("dbo.Advertisements", "LocationId");
            AddForeignKey("dbo.Advertisements", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
    }
}
