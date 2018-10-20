namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlatformGames",
                c => new
                    {
                        Platform_Id = c.String(nullable: false, maxLength: 128),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Platform_Id, t.Game_Id })
                .ForeignKey("dbo.Platforms", t => t.Platform_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Platform_Id)
                .Index(t => t.Game_Id);
            
            AddColumn("dbo.Games", "MetaScore", c => c.Single(nullable: false));
            AddColumn("dbo.Games", "UserScore", c => c.Single(nullable: false));
            AddColumn("dbo.Games", "Developer", c => c.String());
            AddColumn("dbo.Games", "Publisher", c => c.String());
            AddColumn("dbo.Games", "OnlineCapability", c => c.Boolean(nullable: false));
            AddColumn("dbo.Games", "Genre_Id", c => c.Int());
            CreateIndex("dbo.Games", "Genre_Id");
            AddForeignKey("dbo.Games", "Genre_Id", "dbo.Genres", "Id");
            DropColumn("dbo.Games", "PlatformId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "PlatformId", c => c.String());
            DropForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformGames", "Platform_Id", "dbo.Platforms");
            DropForeignKey("dbo.Games", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.PlatformGames", new[] { "Game_Id" });
            DropIndex("dbo.PlatformGames", new[] { "Platform_Id" });
            DropIndex("dbo.Games", new[] { "Genre_Id" });
            DropColumn("dbo.Games", "Genre_Id");
            DropColumn("dbo.Games", "OnlineCapability");
            DropColumn("dbo.Games", "Publisher");
            DropColumn("dbo.Games", "Developer");
            DropColumn("dbo.Games", "UserScore");
            DropColumn("dbo.Games", "MetaScore");
            DropTable("dbo.PlatformGames");
        }
    }
}
