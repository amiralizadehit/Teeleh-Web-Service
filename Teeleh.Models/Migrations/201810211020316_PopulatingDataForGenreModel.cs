namespace Teeleh.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulatingDataForGenreModel : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");

            Sql("INSERT INTO Genres (Id, Name) VALUES (1,'Platformer')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2,'Shooter')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3,'Fighting')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4,'Stealth')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5,'Survival')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6,'Rhythm')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (7,'Survival horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (8,'Metroidvania')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (9,'Text adventures')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (10,'Graphic adventures')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (11,'Visual novels')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (12,'Real-time 3D adventures')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (13,'Action RPG')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (14,'MMORPG')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (15,'Roguelikes')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (16,'Tactical RPG')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (17,'Sandbox RPG')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (18,'First-person party-based RPG')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (19,'Cultural differences')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (20,'Choices')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (21,'Fantasy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (22,'Construction and management simulation')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (23,'Life simulation')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (24,'Vehicle simulation')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (25,'4X game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (26,'Artillery game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (27,'Real-time strategy (RTS)')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (28,'Real-time tactics')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (29,'Multiplayer online battle arena (MOBA)')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (30,'Tower defense')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (31,'Turn-based tactics')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (32,'Wargame')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (33,'Grand strategy wargame')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (34,'Racing')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (35,'Competitive')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (36,'Sports-based fighting')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (37,'MMO')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (38,'Casual game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (39,'Party game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (40,'Programming game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (41,'Logic game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (42,'Trivia game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (43,'Board game / Card game')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (44,'Interactive movie')");

            Sql("SET IDENTITY_INSERT Genres OFF");
        }
        
        public override void Down()
        {
        }
    }
}
