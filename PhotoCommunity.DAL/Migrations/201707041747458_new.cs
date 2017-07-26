namespace PhotoCommunity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Name", c => c.String());
            DropColumn("dbo.Images", "Name1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Name1", c => c.String());
            DropColumn("dbo.Images", "Name");
        }
    }
}
