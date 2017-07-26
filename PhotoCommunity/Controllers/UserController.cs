using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoCommunity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhotoCommunity.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        ApplicationDbContext AppCtx = new ApplicationDbContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            IEnumerable<ApplicationUser> AllUsers = AppCtx.Users;
            foreach (var user in AllUsers)
            {                
                if (!await UserManager.IsInRoleAsync(user.Id, "Admin") && user.EmailConfirmed == true)
                    users.Add(user);
            }
            return View(users);
        }

        [Route("User/{username:regex(^photos-)}")]
        public ActionResult UsersPhotos(string username)
        {
            ///TODO!! 
            ///- проверку на не указанный Ид (типа если вручную вызвали шоу) и если такой картинки не существует

            UserPhotosViewModel UPmodel = new UserPhotosViewModel();
            string UserId = username.Split(new[] { "photos-" }, StringSplitOptions.None).Last();
            if (UserId==User.Identity.GetUserId())
            {
                return RedirectToAction("", "Profile");
            }

            ApplicationUser usr = AppCtx.Users.Where(item => item.Id == UserId).FirstOrDefault();

            if (usr == null)
            {
                return View("Error");
            }

            IEnumerable<Image> imgs = AppCtx.Images.Where(item => item.User.Id == usr.Id);
            UPmodel.user = usr;
            UPmodel.images = imgs;
            return View(UPmodel);
        }               
    }
}