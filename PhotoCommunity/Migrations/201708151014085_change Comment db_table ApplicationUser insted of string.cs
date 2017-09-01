namespace PhotoCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCommentdb_tableApplicationUserinstedofstring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "User_Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comments", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Username", c => c.String());
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.Comments", "User_Id");
        }
    }
}
