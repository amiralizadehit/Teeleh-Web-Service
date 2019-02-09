namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingNotificationModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notifications", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "Status", c => c.Int(nullable: false));
        }
    }
}
