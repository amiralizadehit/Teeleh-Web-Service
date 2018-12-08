namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAdminSession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionKey = c.String(),
                        InitMoment = c.DateTime(nullable: false),
                        DeactivationMoment = c.DateTime(),
                        State = c.Int(nullable: false),
                        Admin_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.Admin_Id)
                .Index(t => t.Admin_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminSessions", "Admin_Id", "dbo.Admins");
            DropIndex("dbo.AdminSessions", new[] { "Admin_Id" });
            DropTable("dbo.AdminSessions");
        }
    }
}
