namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingLocationPropInReq : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests");
            DropIndex("dbo.Locations", new[] { "Request_Id" });
            DropIndex("dbo.Locations", new[] { "Request_Id1" });
            DropIndex("dbo.Locations", new[] { "Request_Id2" });
            AddColumn("dbo.Requests", "LocationProvinceId", c => c.Int());
            AddColumn("dbo.Requests", "LocationCityId", c => c.Int());
            AddColumn("dbo.Requests", "LocationRegionId", c => c.Int());
            CreateIndex("dbo.Requests", "LocationCityId");
            CreateIndex("dbo.Requests", "LocationProvinceId");
            CreateIndex("dbo.Requests", "LocationRegionId");
            AddForeignKey("dbo.Requests", "LocationCityId", "dbo.Locations", "Id");
            AddForeignKey("dbo.Requests", "LocationProvinceId", "dbo.Locations", "Id");
            AddForeignKey("dbo.Requests", "LocationRegionId", "dbo.Locations", "Id");
            DropColumn("dbo.Locations", "Request_Id");
            DropColumn("dbo.Locations", "Request_Id1");
            DropColumn("dbo.Locations", "Request_Id2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Request_Id2", c => c.Int());
            AddColumn("dbo.Locations", "Request_Id1", c => c.Int());
            AddColumn("dbo.Locations", "Request_Id", c => c.Int());
            DropForeignKey("dbo.Requests", "LocationRegionId", "dbo.Locations");
            DropForeignKey("dbo.Requests", "LocationProvinceId", "dbo.Locations");
            DropForeignKey("dbo.Requests", "LocationCityId", "dbo.Locations");
            DropIndex("dbo.Requests", new[] { "LocationRegionId" });
            DropIndex("dbo.Requests", new[] { "LocationProvinceId" });
            DropIndex("dbo.Requests", new[] { "LocationCityId" });
            DropColumn("dbo.Requests", "LocationRegionId");
            DropColumn("dbo.Requests", "LocationCityId");
            DropColumn("dbo.Requests", "LocationProvinceId");
            CreateIndex("dbo.Locations", "Request_Id2");
            CreateIndex("dbo.Locations", "Request_Id1");
            CreateIndex("dbo.Locations", "Request_Id");
            AddForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
