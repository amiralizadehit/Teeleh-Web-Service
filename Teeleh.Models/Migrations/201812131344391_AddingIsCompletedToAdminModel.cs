namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsCompletedToAdminModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "IsCompleted");
        }
    }
}
