namespace Teeleh.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingLocationToDatabase1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'خراسان رضوی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'خراسان شمالی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'تهران')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'آذربایجان شرقی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'آذربایجان غربی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'کرمانشاه')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'هرمزگان')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'سیستان و بلوچستان')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'مرکزی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, N'خراسان جنوبی')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Helsinki')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Tempere')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Turku')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Espoo')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Oulu')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Vantaa')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Lahti')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (NULL, 'Pori')");
            
        }
        
        public override void Down()
        {
        }
    }
}
