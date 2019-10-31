namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeToPSNAccountRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PSNAccountRequests", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PSNAccountRequests", "Type");
        }
    }
}
