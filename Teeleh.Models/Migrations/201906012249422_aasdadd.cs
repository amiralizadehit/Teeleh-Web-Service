namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aasdadd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.Notifications", new[] { "Advertisement_Id" });
            AddColumn("dbo.Notifications", "AdvertisementId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "AdvertisementId");
            AddForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements", "Id");
            DropColumn("dbo.Notifications", "Advertisement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Advertisement_Id", c => c.Int());
            DropForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Notifications", new[] { "AdvertisementId" });
            DropColumn("dbo.Notifications", "AdvertisementId");
            CreateIndex("dbo.Notifications", "Advertisement_Id");
            AddForeignKey("dbo.Notifications", "Advertisement_Id", "dbo.Advertisements", "Id");
        }
    }
}
