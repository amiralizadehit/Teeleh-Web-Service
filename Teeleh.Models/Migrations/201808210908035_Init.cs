namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nonce = c.Int(),
                        InitMoment = c.DateTime(nullable: false),
                        ActivationMoment = c.DateTime(),
                        DeactivationMoment = c.DateTime(),
                        SessionKey = c.String(nullable: false, maxLength: 32),
                        UniqueCode = c.String(),
                        State = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.String(nullable: false, maxLength: 11),
                        Password = c.String(nullable: false),
                        Email = c.String(),
                        PSNId = c.String(),
                        XBOXLive = c.String(),
                        ForgetPassCode = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        UserAvatar_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Avatars", t => t.UserAvatar_Id)
                .Index(t => t.UserAvatar_Id);
            
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AvatarImage = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "UserAvatar_Id", "dbo.Avatars");
            DropIndex("dbo.Sessions", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "UserAvatar_Id" });
            DropTable("dbo.Avatars");
            DropTable("dbo.Users");
            DropTable("dbo.Sessions");
        }
    }
}
