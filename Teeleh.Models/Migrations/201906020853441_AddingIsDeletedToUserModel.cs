namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsDeletedToUserModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
