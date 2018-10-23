namespace Teeleh.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulatingPlatformTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Platforms (Id, Name) VALUES ('PS4','PS4')");
            Sql("INSERT INTO Platforms (Id, Name) VALUES ('XBOX','XBOX')");
            Sql("INSERT INTO Platforms (Id, Name) VALUES ('PC','PC')");
            Sql("INSERT INTO Platforms (Id, Name) VALUES ('Switch','Nintendo Switch')");
            Sql("INSERT INTO Platforms (Id, Name) VALUES ('Android','Android')");
            Sql("INSERT INTO Platforms (Id, Name) VALUES ('iOS','iOS')");
        }
        
        public override void Down()
        {
        }
    }
}
