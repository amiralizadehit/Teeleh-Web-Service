namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAccountRequestToGames : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "PSNAccountRequest_Id", "dbo.PSNAccountRequests");
            DropIndex("dbo.Games", new[] { "PSNAccountRequest_Id" });
            CreateTable(
                "dbo.PSNAccountRequestGames",
                c => new
                    {
                        PSNAccountRequest_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PSNAccountRequest_Id, t.Game_Id })
                .ForeignKey("dbo.PSNAccountRequests", t => t.PSNAccountRequest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.PSNAccountRequest_Id)
                .Index(t => t.Game_Id);
            
            DropColumn("dbo.Games", "PSNAccountRequest_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "PSNAccountRequest_Id", c => c.Int());
            DropForeignKey("dbo.PSNAccountRequestGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PSNAccountRequestGames", "PSNAccountRequest_Id", "dbo.PSNAccountRequests");
            DropIndex("dbo.PSNAccountRequestGames", new[] { "Game_Id" });
            DropIndex("dbo.PSNAccountRequestGames", new[] { "PSNAccountRequest_Id" });
            DropTable("dbo.PSNAccountRequestGames");
            CreateIndex("dbo.Games", "PSNAccountRequest_Id");
            AddForeignKey("dbo.Games", "PSNAccountRequest_Id", "dbo.PSNAccountRequests", "Id");
        }
    }
}
