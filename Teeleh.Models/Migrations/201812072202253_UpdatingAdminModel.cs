namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingAdminModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "FirstName", c => c.String());
            AddColumn("dbo.Admins", "LastName", c => c.String());
            DropColumn("dbo.Admins", "Name");
            DropColumn("dbo.Admins", "Family");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Family", c => c.String());
            AddColumn("dbo.Admins", "Name", c => c.String());
            DropColumn("dbo.Admins", "LastName");
            DropColumn("dbo.Admins", "FirstName");
        }
    }
}
