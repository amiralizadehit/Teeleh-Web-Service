namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAdvertisementModel1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Advertisements", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Advertisements", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Advertisements", "CreatedAt", c => c.DateTime());
        }
    }
}
