namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCaptionFirstLetter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "Caption", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Advertisements", "Caption", c => c.String());
        }
    }
}
