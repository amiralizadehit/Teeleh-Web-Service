namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingSomeRegionForLocation : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (80, N'وکیل آباد')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (80, N'بلوار سجاد')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (80, N'ملک آباد')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (80, N'میدان شهدا')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (80, N'سناباد')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (82, N'نارمک')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (82, N'ونک')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (82, N'سعادت آباد')");
            Sql("INSERT INTO Locations (ParentId, Name) VALUES (82, N'مولوی')");
        }
        
        public override void Down()
        {
        }
    }
}
