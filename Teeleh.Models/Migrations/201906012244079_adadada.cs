namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adadada : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Notifications", new[] { "AdvertisementId" });
            AddColumn("dbo.Notifications", "Advertisement_Id", c => c.Int());
            CreateIndex("dbo.Notifications", "Advertisement_Id");
            AddForeignKey("dbo.Notifications", "Advertisement_Id", "dbo.Advertisements", "Id");
            DropColumn("dbo.Notifications", "AdvertisementId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "AdvertisementId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Notifications", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.Notifications", new[] { "Advertisement_Id" });
            DropColumn("dbo.Notifications", "Advertisement_Id");
            CreateIndex("dbo.Notifications", "AdvertisementId");
            AddForeignKey("dbo.Notifications", "AdvertisementId", "dbo.Advertisements", "Id");
        }
    }
}
