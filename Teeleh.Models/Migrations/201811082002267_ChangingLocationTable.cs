namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingLocationTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Locations", "ParentId");
            AddForeignKey("dbo.Locations", "ParentId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "ParentId", "dbo.Locations");
            DropIndex("dbo.Locations", new[] { "ParentId" });
            AlterColumn("dbo.Locations", "Id", c => c.Int(nullable: false, identity: true));
        }
    }
}
