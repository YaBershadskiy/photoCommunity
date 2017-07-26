using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PhotoCommunity.Models;

namespace PhotoCommunity.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext AppCtx = new ApplicationDbContext();

        public ActionResult Index()
        {
            return RedirectToAction("", "Photo");
        }    

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return View(AppCtx.Users);
        }
    }
}