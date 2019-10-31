namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewChangesToAccountAd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdBookmarks", "PSNAccountAdvertisement_Id", "dbo.PSNAccountAdvertisements");
            DropIndex("dbo.AdBookmarks", new[] { "PSNAccountAdvertisement_Id" });
            DropColumn("dbo.AdBookmarks", "PSNAccountAdvertisement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdBookmarks", "PSNAccountAdvertisement_Id", c => c.Int());
            CreateIndex("dbo.AdBookmarks", "PSNAccountAdvertisement_Id");
            AddForeignKey("dbo.AdBookmarks", "PSNAccountAdvertisement_Id", "dbo.PSNAccountAdvertisements", "Id");
        }
    }
}
