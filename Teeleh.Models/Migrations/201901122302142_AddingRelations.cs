namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRelations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "User_Id", c => c.Int());
            CreateIndex("dbo.Requests", "User_Id");
            AddForeignKey("dbo.Requests", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "User_Id", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "User_Id" });
            DropColumn("dbo.Requests", "User_Id");
        }
    }
}
