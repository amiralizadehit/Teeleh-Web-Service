namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingGameTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Avatar_Id", c => c.Int());
            CreateIndex("dbo.Games", "Avatar_Id");
            AddForeignKey("dbo.Games", "Avatar_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Avatar_Id", "dbo.Images");
            DropIndex("dbo.Games", new[] { "Avatar_Id" });
            DropColumn("dbo.Games", "Avatar_Id");
        }
    }
}
