namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAdBookmarksModel : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AdBookmarks");
            AddPrimaryKey("dbo.AdBookmarks", new[] { "UserId", "AdvertisementId" });
            DropColumn("dbo.AdBookmarks", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdBookmarks", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.AdBookmarks");
            AddPrimaryKey("dbo.AdBookmarks", "Id");
        }
    }
}
