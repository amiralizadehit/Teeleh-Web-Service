namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChangesToNotificationModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Notifications", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "Message", c => c.String());
            DropColumn("dbo.Notifications", "Title");
        }
    }
}
