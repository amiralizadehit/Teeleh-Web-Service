namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableAdvertisementForNotifications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Notifications", new[] { "AdvertisementId" });
            AlterColumn("dbo.Notifications", "AdvertisementId", c => c.Int());
            CreateIndex("dbo.Notifications", "AdvertisementId");
            AddForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Notifications", new[] { "AdvertisementId" });
            AlterColumn("dbo.Notifications", "AdvertisementId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "AdvertisementId");
            AddForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements", "Id");
        }
    }
}
