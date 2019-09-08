namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMorePropertyToPSNAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PSNAccounts", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.PSNAccounts", "HasPlus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PSNAccounts", "HasPlus");
            DropColumn("dbo.PSNAccounts", "Capacity");
        }
    }
}
