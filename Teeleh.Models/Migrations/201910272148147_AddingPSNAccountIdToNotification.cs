namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPSNAccountIdToNotification : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notifications", name: "PSNAccountAdvertisement_Id", newName: "PSNAccountAdvertisementId");
            RenameIndex(table: "dbo.Notifications", name: "IX_PSNAccountAdvertisement_Id", newName: "IX_PSNAccountAdvertisementId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notifications", name: "IX_PSNAccountAdvertisementId", newName: "IX_PSNAccountAdvertisement_Id");
            RenameColumn(table: "dbo.Notifications", name: "PSNAccountAdvertisementId", newName: "PSNAccountAdvertisement_Id");
        }
    }
}
