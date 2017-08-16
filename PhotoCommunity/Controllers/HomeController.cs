using Microsoft.AspNet.Identity;
using PhotoCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoCommunity.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        ApplicationDbContext AppCtx = new ApplicationDbContext();

        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("", "Photo");
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            string userId = User.Identity.GetUserId();
            return View(AppCtx.Users.Where(user => user.Id == userId).FirstOrDefault());
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.PathAndQuery;
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}