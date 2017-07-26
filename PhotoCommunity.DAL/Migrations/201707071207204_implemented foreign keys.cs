namespace PhotoCommunity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class implementedforeignkeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ImgId", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "UserId", c => c.String());
            DropColumn("dbo.Comments", "Img_Id");
            DropColumn("dbo.Images", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "User_Id", c => c.String());
            AddColumn("dbo.Comments", "Img_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Images", "UserId");
            DropColumn("dbo.Comments", "ImgId");
        }
    }
}
