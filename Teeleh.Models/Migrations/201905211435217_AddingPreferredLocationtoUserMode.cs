namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPreferredLocationtoUserMode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "userProvinceId", c => c.Int());
            AddColumn("dbo.Users", "userCityId", c => c.Int());
            CreateIndex("dbo.Users", "userCityId");
            CreateIndex("dbo.Users", "userProvinceId");
            AddForeignKey("dbo.Users", "userCityId", "dbo.Locations", "Id");
            AddForeignKey("dbo.Users", "userProvinceId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "userProvinceId", "dbo.Locations");
            DropForeignKey("dbo.Users", "userCityId", "dbo.Locations");
            DropIndex("dbo.Users", new[] { "userProvinceId" });
            DropIndex("dbo.Users", new[] { "userCityId" });
            DropColumn("dbo.Users", "userCityId");
            DropColumn("dbo.Users", "userProvinceId");
        }
    }
}
