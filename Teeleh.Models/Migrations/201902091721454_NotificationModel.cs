namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Notifications", "AdvertisementId");
            CreateIndex("dbo.Notifications", "UserId");
            AddForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements", "Id");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "AdvertisementId" });
        }
    }
}
