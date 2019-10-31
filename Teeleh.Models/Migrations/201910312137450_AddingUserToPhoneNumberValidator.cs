namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserToPhoneNumberValidator : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserPhoneNumberValidators", "UserId");
            AddForeignKey("dbo.UserPhoneNumberValidators", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPhoneNumberValidators", "UserId", "dbo.Users");
            DropIndex("dbo.UserPhoneNumberValidators", new[] { "UserId" });
        }
    }
}
