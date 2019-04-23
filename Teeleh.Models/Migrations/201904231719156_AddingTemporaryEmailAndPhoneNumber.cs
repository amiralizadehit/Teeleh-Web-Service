namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTemporaryEmailAndPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "TemporaryPhoneNumber", c => c.String(maxLength: 11));
            AddColumn("dbo.Users", "TemporaryEmail", c => c.String());
            AddColumn("dbo.Users", "Token", c => c.Int());
            DropColumn("dbo.Users", "ForgetPassCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ForgetPassCode", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Token");
            DropColumn("dbo.Users", "TemporaryEmail");
            DropColumn("dbo.Users", "TemporaryPhoneNumber");
        }
    }
}
