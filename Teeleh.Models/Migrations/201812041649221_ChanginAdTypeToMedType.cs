namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChanginAdTypeToMedType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "MedType", c => c.Int(nullable: false));
            DropColumn("dbo.Advertisements", "AdType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "AdType", c => c.Int(nullable: false));
            DropColumn("dbo.Advertisements", "MedType");
        }
    }
}
