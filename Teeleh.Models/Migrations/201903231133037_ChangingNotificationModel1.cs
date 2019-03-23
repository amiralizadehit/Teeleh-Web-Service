namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingNotificationModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Message", c => c.String());
            AddColumn("dbo.Notifications", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Type");
            DropColumn("dbo.Notifications", "Message");
        }
    }
}
