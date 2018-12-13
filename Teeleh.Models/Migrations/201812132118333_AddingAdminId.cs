namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAdminId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdminSessions", "Admin_Id", "dbo.Admins");
            DropIndex("dbo.AdminSessions", new[] { "Admin_Id" });
            RenameColumn(table: "dbo.AdminSessions", name: "Admin_Id", newName: "AdminId");
            AlterColumn("dbo.AdminSessions", "AdminId", c => c.Int(nullable: false));
            CreateIndex("dbo.AdminSessions", "AdminId");
            AddForeignKey("dbo.AdminSessions", "AdminId", "dbo.Admins", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminSessions", "AdminId", "dbo.Admins");
            DropIndex("dbo.AdminSessions", new[] { "AdminId" });
            AlterColumn("dbo.AdminSessions", "AdminId", c => c.Int());
            RenameColumn(table: "dbo.AdminSessions", name: "AdminId", newName: "Admin_Id");
            CreateIndex("dbo.AdminSessions", "Admin_Id");
            AddForeignKey("dbo.AdminSessions", "Admin_Id", "dbo.Admins", "Id");
        }
    }
}
