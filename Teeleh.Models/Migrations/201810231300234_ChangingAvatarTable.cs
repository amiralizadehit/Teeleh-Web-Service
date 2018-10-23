namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAvatarTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avatars", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avatars", "Name");
        }
    }
}
