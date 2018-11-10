namespace Teeleh.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingLocationToDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'خراسان رضوی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'خراسان شمالی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'تهران')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'آذربایجان شرقی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'آذربایجان غربی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'کرمانشاه')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'هرمزگان')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'سیستان و بلوچستان')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'مرکزی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'خراسان جنوبی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Helsinki')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Tempere')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Turku')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Espoo')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Oulu')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Vantaa')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Lahti')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Pori')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (1, 'مشهد')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (3, 'تهران')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (9, 'اراک')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (7, 'بندر عباس')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (4, 'تبریز')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (6, 'کرمانشاه')");
        }
        
        public override void Down()
        {
        }
    }
}
