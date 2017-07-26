namespace PhotoCommunity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Description");
        }
    }
}
