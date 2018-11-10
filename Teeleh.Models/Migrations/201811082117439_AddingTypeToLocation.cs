namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTypeToLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Type");
        }
    }
}
