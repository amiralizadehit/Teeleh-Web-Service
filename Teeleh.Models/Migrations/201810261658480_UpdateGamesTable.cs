namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGamesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Game_Id", "dbo.Games");
            DropIndex("dbo.Images", new[] { "Game_Id" });
            DropColumn("dbo.Images", "Game_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Game_Id", c => c.Int());
            CreateIndex("dbo.Images", "Game_Id");
            AddForeignKey("dbo.Images", "Game_Id", "dbo.Games", "Id");
        }
    }
}
