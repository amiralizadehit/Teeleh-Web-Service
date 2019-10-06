namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTempPhoneNumberFromUserModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserPhoneNumberValidators", "TargetNumber", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.UserPhoneNumberValidators", "SecurityToken", c => c.String(nullable: false));
            DropColumn("dbo.Users", "TemporaryPhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "TemporaryPhoneNumber", c => c.String(maxLength: 11));
            AlterColumn("dbo.UserPhoneNumberValidators", "SecurityToken", c => c.Int(nullable: false));
            AlterColumn("dbo.UserPhoneNumberValidators", "TargetNumber", c => c.String(nullable: false));
        }
    }
}
