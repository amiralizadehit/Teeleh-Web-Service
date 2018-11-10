namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChangesToDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "isDeleted");
        }
    }
}
