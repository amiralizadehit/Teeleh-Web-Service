namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFCMToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "FCMToke", c => c.String());
            AddColumn("dbo.Sessions", "SessionPlatform", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "SessionPlatform");
            DropColumn("dbo.Sessions", "FCMToke");
        }
    }
}
