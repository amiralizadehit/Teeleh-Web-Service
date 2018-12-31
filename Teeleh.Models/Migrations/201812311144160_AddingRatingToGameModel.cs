namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRatingToGameModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Rating");
        }
    }
}
