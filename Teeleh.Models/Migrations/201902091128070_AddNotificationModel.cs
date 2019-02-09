namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AdvertisementId = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sessions", "FCMToken", c => c.String());
            DropColumn("dbo.Sessions", "FCMToke");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "FCMToke", c => c.String());
            DropColumn("dbo.Sessions", "FCMToken");
            DropTable("dbo.Notifications");
        }
    }
}
