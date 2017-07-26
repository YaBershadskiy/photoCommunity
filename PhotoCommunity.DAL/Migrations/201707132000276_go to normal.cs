namespace PhotoCommunity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gotonormal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Likes", "rgerg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Likes", "rgerg", c => c.String());
        }
    }
}
