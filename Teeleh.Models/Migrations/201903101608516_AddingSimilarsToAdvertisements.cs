namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSimilarsToAdvertisements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "Advertisement_Id", c => c.Int());
            CreateIndex("dbo.Advertisements", "Advertisement_Id");
            AddForeignKey("dbo.Advertisements", "Advertisement_Id", "dbo.Advertisements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.Advertisements", new[] { "Advertisement_Id" });
            DropColumn("dbo.Advertisements", "Advertisement_Id");
        }
    }
}
