namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeAdditionalChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SecurityToken", c => c.Int());
            DropColumn("dbo.Users", "Token");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Token", c => c.Int());
            DropColumn("dbo.Users", "SecurityToken");
        }
    }
}
