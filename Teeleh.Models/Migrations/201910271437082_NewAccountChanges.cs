namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAccountChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PSNAccountGames", "PSNAccount_Id", "dbo.PSNAccounts");
            DropForeignKey("dbo.PSNAccountGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PSNAccounts", "User_Id", "dbo.Users");
            DropIndex("dbo.PSNAccounts", new[] { "User_Id" });
            DropIndex("dbo.PSNAccountGames", new[] { "PSNAccount_Id" });
            DropIndex("dbo.PSNAccountGames", new[] { "Game_Id" });
            CreateTable(
                "dbo.PSNAccountAdvertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Capacity = c.Int(),
                        Type = c.Int(nullable: false),
                        Region = c.Int(nullable: false),
                        HasPlus = c.Boolean(),
                        Caption = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                        UserImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.UserImage_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserImage_Id);
            
            CreateTable(
                "dbo.PSNAccountAdvertisementGames",
                c => new
                    {
                        PSNAccountAdvertisement_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PSNAccountAdvertisement_Id, t.Game_Id })
                .ForeignKey("dbo.PSNAccountAdvertisements", t => t.PSNAccountAdvertisement_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.PSNAccountAdvertisement_Id)
                .Index(t => t.Game_Id);
            
            AddColumn("dbo.AdBookmarks", "PSNAccountAdvertisement_Id", c => c.Int());
            AddColumn("dbo.Notifications", "PSNAccountAdvertisement_Id", c => c.Int());
            CreateIndex("dbo.AdBookmarks", "PSNAccountAdvertisement_Id");
            CreateIndex("dbo.Notifications", "PSNAccountAdvertisement_Id");
            AddForeignKey("dbo.Notifications", "PSNAccountAdvertisement_Id", "dbo.PSNAccountAdvertisements", "Id");
            AddForeignKey("dbo.AdBookmarks", "PSNAccountAdvertisement_Id", "dbo.PSNAccountAdvertisements", "Id");
            DropTable("dbo.PSNAccounts");
            DropTable("dbo.PSNAccountGames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PSNAccountGames",
                c => new
                    {
                        PSNAccount_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PSNAccount_Id, t.Game_Id });
            
            CreateTable(
                "dbo.PSNAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Capacity = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        HasPlus = c.Boolean(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.PSNAccountAdvertisements", "UserImage_Id", "dbo.Images");
            DropForeignKey("dbo.PSNAccountAdvertisements", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AdBookmarks", "PSNAccountAdvertisement_Id", "dbo.PSNAccountAdvertisements");
            DropForeignKey("dbo.Notifications", "PSNAccountAdvertisement_Id", "dbo.PSNAccountAdvertisements");
            DropForeignKey("dbo.PSNAccountAdvertisementGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PSNAccountAdvertisementGames", "PSNAccountAdvertisement_Id", "dbo.PSNAccountAdvertisements");
            DropIndex("dbo.PSNAccountAdvertisementGames", new[] { "Game_Id" });
            DropIndex("dbo.PSNAccountAdvertisementGames", new[] { "PSNAccountAdvertisement_Id" });
            DropIndex("dbo.Notifications", new[] { "PSNAccountAdvertisement_Id" });
            DropIndex("dbo.PSNAccountAdvertisements", new[] { "UserImage_Id" });
            DropIndex("dbo.PSNAccountAdvertisements", new[] { "User_Id" });
            DropIndex("dbo.AdBookmarks", new[] { "PSNAccountAdvertisement_Id" });
            DropColumn("dbo.Notifications", "PSNAccountAdvertisement_Id");
            DropColumn("dbo.AdBookmarks", "PSNAccountAdvertisement_Id");
            DropTable("dbo.PSNAccountAdvertisementGames");
            DropTable("dbo.PSNAccountAdvertisements");
            CreateIndex("dbo.PSNAccountGames", "Game_Id");
            CreateIndex("dbo.PSNAccountGames", "PSNAccount_Id");
            CreateIndex("dbo.PSNAccounts", "User_Id");
            AddForeignKey("dbo.PSNAccounts", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PSNAccountGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PSNAccountGames", "PSNAccount_Id", "dbo.PSNAccounts", "Id", cascadeDelete: true);
        }
    }
}
