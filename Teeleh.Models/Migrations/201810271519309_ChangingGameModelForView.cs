namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingGameModelForView : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "MetaScore", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "MetaScore", c => c.Single(nullable: false));
        }
    }
}
