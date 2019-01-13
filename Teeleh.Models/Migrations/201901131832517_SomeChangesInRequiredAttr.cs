namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChangesInRequiredAttr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Requests", "User_Id", "dbo.Users");
            DropIndex("dbo.Advertisements", new[] { "User_Id" });
            DropIndex("dbo.Requests", new[] { "User_Id" });
            AlterColumn("dbo.Advertisements", "User_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Advertisements", "User_Id");
            CreateIndex("dbo.Requests", "User_Id");
            AddForeignKey("dbo.Advertisements", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Advertisements", "User_Id", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "User_Id" });
            DropIndex("dbo.Advertisements", new[] { "User_Id" });
            AlterColumn("dbo.Requests", "User_Id", c => c.Int());
            AlterColumn("dbo.Advertisements", "User_Id", c => c.Int());
            CreateIndex("dbo.Requests", "User_Id");
            CreateIndex("dbo.Advertisements", "User_Id");
            AddForeignKey("dbo.Requests", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Advertisements", "User_Id", "dbo.Users", "Id");
        }
    }
}
