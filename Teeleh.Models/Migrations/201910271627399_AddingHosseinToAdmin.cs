namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingHosseinToAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Admins ON");
            Sql("INSERT INTO Admins (Id, FirstName, LastName, Username, Password) " +
                "VALUES (6,'Hossein','Fardpour','hosseinfardpour','123456789')");
        }
        
        public override void Down()
        {
        }
    }
}
