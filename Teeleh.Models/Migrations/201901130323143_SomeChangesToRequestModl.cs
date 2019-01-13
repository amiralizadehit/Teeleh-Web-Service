namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChangesToRequestModl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests");
            DropForeignKey("dbo.Requests", "Game_Id", "dbo.Games");
            DropIndex("dbo.Locations", new[] { "Request_Id" });
            DropIndex("dbo.Locations", new[] { "Request_Id1" });
            DropIndex("dbo.Locations", new[] { "Request_Id2" });
            DropIndex("dbo.Requests", new[] { "Game_Id" });
            RenameColumn(table: "dbo.Requests", name: "Game_Id", newName: "GameId");
            AlterColumn("dbo.Requests", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.Locations", "Request_Id");
            CreateIndex("dbo.Locations", "Request_Id1");
            CreateIndex("dbo.Locations", "Request_Id2");
            CreateIndex("dbo.Requests", "GameId");
            AddForeignKey("dbo.Locations", "Request_Id", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests", "Id");
            AddForeignKey("dbo.Requests", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "GameId", "dbo.Games");
            DropForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Requests", new[] { "GameId" });
            DropIndex("dbo.Locations", new[] { "Request_Id2" });
            DropIndex("dbo.Locations", new[] { "Request_Id1" });
            DropIndex("dbo.Locations", new[] { "Request_Id" });
            AlterColumn("dbo.Requests", "GameId", c => c.Int());
            RenameColumn(table: "dbo.Requests", name: "GameId", newName: "Game_Id");
            CreateIndex("dbo.Requests", "Game_Id");
            CreateIndex("dbo.Locations", "Request_Id2");
            CreateIndex("dbo.Locations", "Request_Id1");
            CreateIndex("dbo.Locations", "Request_Id");
            AddForeignKey("dbo.Requests", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
