namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingGameModelTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Avatar_Id", "dbo.Images");
            DropIndex("dbo.Games", new[] { "Avatar_Id" });
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Games", "Avatar_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "Avatar_Id");
            AddForeignKey("dbo.Games", "Avatar_Id", "dbo.Images", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Avatar_Id", "dbo.Images");
            DropIndex("dbo.Games", new[] { "Avatar_Id" });
            AlterColumn("dbo.Games", "Avatar_Id", c => c.Int());
            AlterColumn("dbo.Games", "Name", c => c.String());
            CreateIndex("dbo.Games", "Avatar_Id");
            AddForeignKey("dbo.Games", "Avatar_Id", "dbo.Images", "Id");
        }
    }
}
