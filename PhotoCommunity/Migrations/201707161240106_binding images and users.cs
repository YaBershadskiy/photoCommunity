namespace PhotoCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bindingimagesandusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Likes", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Images", "User_Id");
            CreateIndex("dbo.Likes", "User_Id");
            AddForeignKey("dbo.Images", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Images", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "UserId", c => c.String());
            DropForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Images", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropIndex("dbo.Images", new[] { "User_Id" });
            AlterColumn("dbo.Likes", "User_Id", c => c.String());
            DropColumn("dbo.Images", "User_Id");
        }
    }
}
