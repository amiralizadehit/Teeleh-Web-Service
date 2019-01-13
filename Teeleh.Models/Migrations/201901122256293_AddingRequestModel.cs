namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRequestModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilterType = c.Int(nullable: false),
                        ReqMode = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        MinPrice = c.Single(nullable: false),
                        MaxPrice = c.Single(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            AddColumn("dbo.Platforms", "Request_Id", c => c.Int());
            AddColumn("dbo.Locations", "Request_Id", c => c.Int());
            AddColumn("dbo.Locations", "Request_Id1", c => c.Int());
            AddColumn("dbo.Locations", "Request_Id2", c => c.Int());
            CreateIndex("dbo.Locations", "Request_Id");
            CreateIndex("dbo.Locations", "Request_Id1");
            CreateIndex("dbo.Locations", "Request_Id2");
            CreateIndex("dbo.Platforms", "Request_Id");
            AddForeignKey("dbo.Locations", "Request_Id", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests", "Id");
            AddForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests", "Id");
            AddForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id2", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id1", "dbo.Requests");
            DropForeignKey("dbo.Locations", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Requests", "Game_Id", "dbo.Games");
            DropIndex("dbo.Platforms", new[] { "Request_Id" });
            DropIndex("dbo.Locations", new[] { "Request_Id2" });
            DropIndex("dbo.Locations", new[] { "Request_Id1" });
            DropIndex("dbo.Locations", new[] { "Request_Id" });
            DropIndex("dbo.Requests", new[] { "Game_Id" });
            DropColumn("dbo.Locations", "Request_Id2");
            DropColumn("dbo.Locations", "Request_Id1");
            DropColumn("dbo.Locations", "Request_Id");
            DropColumn("dbo.Platforms", "Request_Id");
            DropTable("dbo.Requests");
        }
    }
}
