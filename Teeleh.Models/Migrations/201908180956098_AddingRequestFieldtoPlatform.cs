namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRequestFieldtoPlatform : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Platforms", new[] { "Request_Id" });
            CreateTable(
                "dbo.PlatformRequests",
                c => new
                    {
                        Platform_Id = c.String(nullable: false, maxLength: 128),
                        Request_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Platform_Id, t.Request_Id })
                .ForeignKey("dbo.Platforms", t => t.Platform_Id, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.Request_Id, cascadeDelete: true)
                .Index(t => t.Platform_Id)
                .Index(t => t.Request_Id);
            
            DropColumn("dbo.Platforms", "Request_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Platforms", "Request_Id", c => c.Int());
            DropForeignKey("dbo.PlatformRequests", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.PlatformRequests", "Platform_Id", "dbo.Platforms");
            DropIndex("dbo.PlatformRequests", new[] { "Request_Id" });
            DropIndex("dbo.PlatformRequests", new[] { "Platform_Id" });
            DropTable("dbo.PlatformRequests");
            CreateIndex("dbo.Platforms", "Request_Id");
            AddForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
