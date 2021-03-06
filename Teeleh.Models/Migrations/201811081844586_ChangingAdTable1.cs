namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAdTable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Advertisements", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Advertisements", new[] { "Game_Id" });
            DropIndex("dbo.Advertisements", new[] { "Location_Id" });
            RenameColumn(table: "dbo.Advertisements", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.Advertisements", name: "Location_Id", newName: "LocationRegionId");
            RenameColumn(table: "dbo.Advertisements", name: "Platform_Id", newName: "PlatformId");
            AlterColumn("dbo.Advertisements", "GameId", c => c.Int(nullable: false));
            AlterColumn("dbo.Advertisements", "LocationRegionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Advertisements", "GameId");
            CreateIndex("dbo.Advertisements", "LocationRegionId");
            AddForeignKey("dbo.Advertisements", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Advertisements", "LocationRegionId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "LocationRegionId", "dbo.Locations");
            DropForeignKey("dbo.Advertisements", "GameId", "dbo.Games");
            DropIndex("dbo.Advertisements", new[] { "LocationRegionId" });
            DropIndex("dbo.Advertisements", new[] { "GameId" });
            AlterColumn("dbo.Advertisements", "LocationRegionId", c => c.Int());
            AlterColumn("dbo.Advertisements", "GameId", c => c.Int());
            RenameColumn(table: "dbo.Advertisements", name: "PlatformId", newName: "Platform_Id");
            RenameColumn(table: "dbo.Advertisements", name: "LocationRegionId", newName: "Location_Id");
            RenameColumn(table: "dbo.Advertisements", name: "GameId", newName: "Game_Id");
            CreateIndex("dbo.Advertisements", "Location_Id");
            CreateIndex("dbo.Advertisements", "Game_Id");
            AddForeignKey("dbo.Advertisements", "Location_Id", "dbo.Locations", "Id");
            AddForeignKey("dbo.Advertisements", "Game_Id", "dbo.Games", "Id");
        }
    }
}
