namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMoreImagesToGames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Cover_Id", c => c.Int());
            AddColumn("dbo.Images", "Game_Id", c => c.Int());
            CreateIndex("dbo.Games", "Cover_Id");
            CreateIndex("dbo.Images", "Game_Id");
            AddForeignKey("dbo.Games", "Cover_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.Images", "Game_Id", "dbo.Games", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Games", "Cover_Id", "dbo.Images");
            DropIndex("dbo.Images", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Cover_Id" });
            DropColumn("dbo.Images", "Game_Id");
            DropColumn("dbo.Games", "Cover_Id");
        }
    }
}
