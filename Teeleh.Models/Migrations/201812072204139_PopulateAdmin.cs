namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Admins ON");
            Sql("INSERT INTO Admins (Id, FirstName, LastName, Username, Password) " +
                "VALUES (1,'Amir','Alizadeh','amiralizadeh','123456789')");
            Sql("INSERT INTO Admins (Id, FirstName, LastName, Username, Password) " +
                "VALUES (2,'Vahid','Ranandeh','vahidranandeh','123456789')");
            Sql("INSERT INTO Admins (Id, FirstName, LastName, Username, Password) " +
                "VALUES (3,'Ehsan','Vahidi Far','ehsanvahidifar','123456789')");
            Sql("INSERT INTO Admins (Id, FirstName, LastName, Username, Password) " +
                "VALUES (4,'Mohsen','Akbari','mohsenakbari','123456789')");
            Sql("INSERT INTO Admins (Id, FirstName, LastName, Username, Password) " +
                "VALUES (5,'Iman','Nourbakhsh','imannourbakhsh','123456789')");
        }
        
        public override void Down()
        {
        }
    }
}
