namespace PhotoCommunity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ewfegeg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Img_Id = c.Int(nullable: false),
                        Username = c.String(),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Name1 = c.String(),
                        Img = c.Binary(),
                        Rate = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Image_Id", "dbo.Images");
            DropIndex("dbo.Comments", new[] { "Image_Id" });
            DropTable("dbo.Images");
            DropTable("dbo.Comments");
        }
    }
}
