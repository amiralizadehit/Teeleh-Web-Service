namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAdvertisementModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "PlatformId", "dbo.Platforms");
            DropIndex("dbo.Advertisements", "IX_Platform_Id");
            AlterColumn("dbo.Advertisements", "PlatformId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Advertisements", "PlatformId");
            AddForeignKey("dbo.Advertisements", "PlatformId", "dbo.Platforms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "PlatformId", "dbo.Platforms");
            DropIndex("dbo.Advertisements", new[] { "PlatformId" });
            AlterColumn("dbo.Advertisements", "PlatformId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Advertisements", "PlatformId");
            AddForeignKey("dbo.Advertisements", "PlatformId", "dbo.Platforms", "Id");
        }
    }
}
