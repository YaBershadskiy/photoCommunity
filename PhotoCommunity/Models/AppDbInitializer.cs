using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoCommunity;
using PhotoCommunity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;

namespace RolesIdentityApp.Models
{
    public class AppDbCtxInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            ApplicationDbContext.Create();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var adminRole = new IdentityRole { Name = "Admin" };
                roleManager.Create(adminRole);

                //Here we create a Admin super user who will maintain the website                  

                var adminUser = new ApplicationUser();
                adminUser.Name = "Admin";
                adminUser.Avatar = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/admin-avatar.png"));
                adminUser.UserName = "admin_PhotoComunity@gmail.com";
                adminUser.Email = "admin_PhotoComunity@gmail.com";
                adminUser.EmailConfirmed = true;

                var chkUser = userManager.Create(adminUser, "123321qW.");

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(adminUser.Id, "Admin");

                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var userRole = new IdentityRole { Name = "User" };
                roleManager.Create(userRole);
            }

            context.SaveChanges();
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
    }
}