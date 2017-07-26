namespace PhotoCommunity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoCommunity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PhotoCommunity.Models.ApplicationDbContext";
        }

        protected override void Seed(PhotoCommunity.Models.ApplicationDbContext context)
        {
            
        }
    }
}
