namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableFieldForValidatedAt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserPhoneNumberValidators", "ValidatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserPhoneNumberValidators", "ValidatedAt", c => c.DateTime(nullable: false));
        }
    }
}
