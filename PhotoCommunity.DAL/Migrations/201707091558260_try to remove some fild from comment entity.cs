namespace PhotoCommunity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trytoremovesomefildfromcommententity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "ImgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ImgId", c => c.Int(nullable: false));
        }
    }
}
