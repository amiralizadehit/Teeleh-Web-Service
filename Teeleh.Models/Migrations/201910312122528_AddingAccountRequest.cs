namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAccountRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PSNAccountRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Region = c.Int(),
                        Capacity = c.Int(),
                        MinPrice = c.Single(nullable: false),
                        MaxPrice = c.Single(nullable: false),
                        HasPlus = c.Boolean(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Games", "PSNAccountRequest_Id", c => c.Int());
            CreateIndex("dbo.Games", "PSNAccountRequest_Id");
            AddForeignKey("dbo.Games", "PSNAccountRequest_Id", "dbo.PSNAccountRequests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PSNAccountRequests", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Games", "PSNAccountRequest_Id", "dbo.PSNAccountRequests");
            DropIndex("dbo.PSNAccountRequests", new[] { "User_Id" });
            DropIndex("dbo.Games", new[] { "PSNAccountRequest_Id" });
            DropColumn("dbo.Games", "PSNAccountRequest_Id");
            DropTable("dbo.PSNAccountRequests");
        }
    }
}
