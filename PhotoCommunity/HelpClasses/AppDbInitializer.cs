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

                var adminRole = new IdentityRole { Name = "Admin" };
                roleManager.Create(adminRole);

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


            /*********************Adding fake accounts and photos to the PhotoCommunity****************************/
            ApplicationUser user;
            Image image;
            for (int i = 0; i < 10; i++)
            {
                user = new ApplicationUser();
                user.Name = "John";
                user.Surname = "Smith " +(i+1);
                user.Email = "user" + i + "@exmpl.com";
                user.PhoneNumber = "0000000000";
                user.UserName = "user" + i + "@exmpl.com";
                user.EmailConfirmed = true;
                user.Avatar = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/user-avatar.png"));
                user.About = "Robot";
                userManager.Create(user, "123321qW");
                userManager.AddToRole(user.Id, "User");

                for (int j = 0; j < 3; j++)
                {
                    image = new Image();
                    image.Name = "Image" + ((i * 3 + j)+1);
                    image.User = user;
                    image.Date = DateTime.Now;
                    image.Description = "Good photo";
                    image.Img = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/some-image.jpg"));
                    context.Images.Add(image);
                }
            }


            /*******************************************************************************/

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