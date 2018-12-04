namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAdTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "MedType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "MedType");
        }
    }
}
