namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserPhoneValidatorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPhoneNumberValidators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TargetNumber = c.String(nullable: false),
                        SecurityToken = c.Int(nullable: false),
                        IsValidated = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ValidatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPhoneNumberValidators");
        }
    }
}
