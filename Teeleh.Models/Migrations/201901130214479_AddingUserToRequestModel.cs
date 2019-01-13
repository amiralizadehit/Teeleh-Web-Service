namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserToRequestModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Platforms", new[] { "Request_Id" });
            CreateIndex("dbo.Platforms", "Request_Id");
            AddForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Platforms", new[] { "Request_Id" });
            CreateIndex("dbo.Platforms", "Request_Id");
            AddForeignKey("dbo.Platforms", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
