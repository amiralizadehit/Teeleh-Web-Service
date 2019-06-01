namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNewChangesToNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "AvatarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "AvatarId");
            AddForeignKey("dbo.Notifications", "AvatarId", "dbo.Images", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "AvatarId", "dbo.Images");
            DropIndex("dbo.Notifications", new[] { "AvatarId" });
            DropColumn("dbo.Notifications", "AvatarId");
        }
    }
}
