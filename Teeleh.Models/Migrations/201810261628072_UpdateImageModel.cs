namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateImageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Images", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "UpdatedAt");
            DropColumn("dbo.Images", "CreatedAt");
            DropColumn("dbo.Images", "Type");
        }
    }
}
