namespace PhotoCommunity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypeofImageUser_Idtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "User_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "User_Id", c => c.Int(nullable: false));
        }
    }
}
