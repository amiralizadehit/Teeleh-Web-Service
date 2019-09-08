namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPSNAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PSNAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Type = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PSNAccountGames",
                c => new
                    {
                        PSNAccount_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PSNAccount_Id, t.Game_Id })
                .ForeignKey("dbo.PSNAccounts", t => t.PSNAccount_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.PSNAccount_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PSNAccounts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PSNAccountGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PSNAccountGames", "PSNAccount_Id", "dbo.PSNAccounts");
            DropIndex("dbo.PSNAccountGames", new[] { "Game_Id" });
            DropIndex("dbo.PSNAccountGames", new[] { "PSNAccount_Id" });
            DropIndex("dbo.PSNAccounts", new[] { "User_Id" });
            DropTable("dbo.PSNAccountGames");
            DropTable("dbo.PSNAccounts");
        }
    }
}
