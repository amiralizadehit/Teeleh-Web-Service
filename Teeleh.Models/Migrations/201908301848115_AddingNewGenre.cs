namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNewGenre : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("INSERT INTO Genres (Id, Name) VALUES (45,'Sports')");
            Sql("SET IDENTITY_INSERT Genres OFF");
        }
        
        public override void Down()
        {
        }
    }
}
