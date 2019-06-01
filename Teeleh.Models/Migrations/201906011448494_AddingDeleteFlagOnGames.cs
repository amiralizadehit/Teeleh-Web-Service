namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeleteFlagOnGames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "isDeleted");
        }
    }
}
