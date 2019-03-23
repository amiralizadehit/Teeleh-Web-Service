namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAdBookmarksModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdBookmarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AdvertisementId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Advertisements", t => t.AdvertisementId)
                .Index(t => t.UserId)
                .Index(t => t.AdvertisementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdBookmarks", "AdvertisementId", "dbo.Advertisements");
            DropForeignKey("dbo.AdBookmarks", "UserId", "dbo.Users");
            DropIndex("dbo.AdBookmarks", new[] { "AdvertisementId" });
            DropIndex("dbo.AdBookmarks", new[] { "UserId" });
            DropTable("dbo.AdBookmarks");
        }
    }
}
