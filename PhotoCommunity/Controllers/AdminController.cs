using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoCommunity.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace PhotoCommunity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AllUsers db = new AllUsers();
        private ApplicationDbContext allDb = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

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

        // GET: AspNetUsers
        public async Task<ActionResult> Index()
        {
            List<AdminUsersListViewModel> users = new List<AdminUsersListViewModel>();
                       
            foreach (AspNetUsers item in db.AspNetUsers)
            {
                if ( await UserManager.IsInRoleAsync(item.Id,"User"))
                {
                    users.Add(DbTable2Model(item));
                }
            }
            return View(users);
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(DbTable2Model(aspNetUsers));
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Avatar,About,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(DbTable2Model(aspNetUsers));
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(DbTable2Model(aspNetUsers));
        }

        // POST: AspNetUsers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Avatar,About,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DbTable2Model(aspNetUsers));
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(DbTable2Model(aspNetUsers));
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeletePhoto(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image photo = db.Image.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("DeletePhoto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhotoConfirmed(int photoId)
        {
            Image photo = db.Image.Find(photoId);
            foreach (var comment in photo.Comments)
            {
                db.Comment.Remove(comment);
            }
            db.Image.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index","Photo");
        }

         // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int commentId)
        {
            Comment comment = allDb.Comments.Find(commentId);
            allDb.Comments.Remove(comment);
            allDb.SaveChanges();
            return PartialView("~/Views/Photo/AddComment.cshtml",allDb.Comments);
        }

        public async Task<ActionResult> Sort(string sort,string dir)
        {
            List<AdminUsersListViewModel> users = new List<AdminUsersListViewModel>();

            foreach (AspNetUsers item in db.AspNetUsers)
            {
                if (await UserManager.IsInRoleAsync(item.Id, "User"))
                {
                    users.Add(DbTable2Model(item));
                }
            }
            if (dir=="asc")
            {
                switch (sort)
                {
                    case "name":
                        users = users.OrderBy(prop => prop.Name).ToList<AdminUsersListViewModel>();
                        break;
                    case "surname":
                        users = users.OrderBy(prop => prop.Surname).ToList<AdminUsersListViewModel>();
                        break;
                    case "email":
                        users = users.OrderBy(prop => prop.Email).ToList<AdminUsersListViewModel>();
                        break;
                    case "phonenumber":
                        users = users.OrderBy(prop => prop.PhoneNumber).ToList<AdminUsersListViewModel>();
                        break;
                    case "emailconfrimed":
                        users = users.OrderBy(prop => prop.EmailConfirmed).ToList<AdminUsersListViewModel>();
                        break;
                    case "about":
                        users = users.OrderBy(prop => prop.About).ToList<AdminUsersListViewModel>();
                        break;
                    case "password":
                        users = users.OrderBy(prop => prop.PasswordHash).ToList<AdminUsersListViewModel>();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (sort)
                {
                    case "name":
                        users = users.OrderByDescending(prop => prop.Name).ToList<AdminUsersListViewModel>();
                        break;
                    case "surname":
                        users = users.OrderByDescending(prop => prop.Surname).ToList<AdminUsersListViewModel>();
                        break;
                    case "email":
                        users = users.OrderByDescending(prop => prop.Email).ToList<AdminUsersListViewModel>();
                        break;
                    case "phonenumber":
                        users = users.OrderByDescending(prop => prop.PhoneNumber).ToList<AdminUsersListViewModel>();
                        break;
                    case "emailconfrimed":
                        users = users.OrderByDescending(prop => prop.EmailConfirmed).ToList<AdminUsersListViewModel>();
                        break;
                    case "about":
                        users = users.OrderByDescending(prop => prop.About).ToList<AdminUsersListViewModel>();
                        break;
                    case "password":
                        users = users.OrderByDescending(prop => prop.PasswordHash).ToList<AdminUsersListViewModel>();
                        break;
                    default:
                        break;
                }
            }
            
            return PartialView(users);
        }

        public async Task<ActionResult> Search(string searchText)
        {
            List<AdminUsersListViewModel> users = new List<AdminUsersListViewModel>();
            var needUsers = from user in db.AspNetUsers
                            where user.Name.Contains(searchText) ||
                            user.Surname.Contains(searchText) ||
                            user.Email.Contains(searchText) ||
                            user.PhoneNumber.Contains(searchText)
                            select user;
            foreach (AspNetUsers item in needUsers)
            {
                if (await UserManager.IsInRoleAsync(item.Id, "User"))
                {
                    users.Add(DbTable2Model(item));
                }
            }

            return PartialView("Sort",users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private AdminUsersListViewModel DbTable2Model(AspNetUsers aspNetUsers)
        {
            AdminUsersListViewModel user = new AdminUsersListViewModel();
            user.Id = aspNetUsers.Id;
            user.Name = aspNetUsers.Name;
            user.Surname = aspNetUsers.Surname;
            user.Email = aspNetUsers.Email;
            user.Avatar = aspNetUsers.Avatar;
            user.EmailConfirmed = aspNetUsers.EmailConfirmed;
            user.PasswordHash = aspNetUsers.PasswordHash;
            user.PhoneNumber = aspNetUsers.PhoneNumber;
            user.About = aspNetUsers.About;
            return user;
        }


    }
}
