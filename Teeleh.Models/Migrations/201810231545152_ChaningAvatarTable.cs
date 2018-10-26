namespace Teeleh.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChaningAvatarTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserAvatar_Id", "dbo.Avatars");
            DropForeignKey("dbo.Games", "Avatar_Id", "dbo.Avatars");
            DropIndex("dbo.Users", new[] { "UserAvatar_Id" });
            DropIndex("dbo.Games", new[] { "Avatar_Id" });
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImagePath = c.String(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            AddColumn("dbo.Users", "UserImage_Id", c => c.Int());
            CreateIndex("dbo.Users", "UserImage_Id");
            AddForeignKey("dbo.Users", "UserImage_Id", "dbo.Images", "Id");
            DropColumn("dbo.Users", "UserAvatar_Id");
            DropColumn("dbo.Games", "Avatar_Id");
            DropTable("dbo.Avatars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AvatarImage = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "Avatar_Id", c => c.Int());
            AddColumn("dbo.Users", "UserAvatar_Id", c => c.Int());
            DropForeignKey("dbo.Images", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Users", "UserImage_Id", "dbo.Images");
            DropIndex("dbo.Images", new[] { "Game_Id" });
            DropIndex("dbo.Users", new[] { "UserImage_Id" });
            DropColumn("dbo.Users", "UserImage_Id");
            DropTable("dbo.Images");
            CreateIndex("dbo.Games", "Avatar_Id");
            CreateIndex("dbo.Users", "UserAvatar_Id");
            AddForeignKey("dbo.Games", "Avatar_Id", "dbo.Avatars", "Id");
            AddForeignKey("dbo.Users", "UserAvatar_Id", "dbo.Avatars", "Id");
        }
    }
}
