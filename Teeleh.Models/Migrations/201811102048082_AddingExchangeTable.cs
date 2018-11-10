namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingExchangeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.Games", new[] { "Advertisement_Id" });
            CreateTable(
                "dbo.Exchanges",
                c => new
                    {
                        AdvertisementId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AdvertisementId, t.GameId })
                .ForeignKey("dbo.Advertisements", t => t.AdvertisementId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.AdvertisementId)
                .Index(t => t.GameId);
            
            DropColumn("dbo.Games", "Advertisement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Advertisement_Id", c => c.Int());
            DropForeignKey("dbo.Exchanges", "GameId", "dbo.Games");
            DropForeignKey("dbo.Exchanges", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Exchanges", new[] { "GameId" });
            DropIndex("dbo.Exchanges", new[] { "AdvertisementId" });
            DropTable("dbo.Exchanges");
            CreateIndex("dbo.Games", "Advertisement_Id");
            AddForeignKey("dbo.Games", "Advertisement_Id", "dbo.Advertisements", "Id");
        }
    }
}
