namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionAddedToPSNAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PSNAccounts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PSNAccounts", "Description");
        }
    }
}
