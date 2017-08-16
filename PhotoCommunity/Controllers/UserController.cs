using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PhotoCommunity.Models;
using System.IO;
using System.Collections.Generic;
using PagedList;

namespace PhotoCommunity.Controllers
{
    [Authorize]
    [Culture]
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        ApplicationDbContext db = new ApplicationDbContext();

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        [AllowAnonymous]
        public async Task<ActionResult> Index(int? page)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            IEnumerable<ApplicationUser> AllUsers = db.Users;
            foreach (var user in AllUsers)
            {
                if (!await UserManager.IsInRoleAsync(user.Id, "Admin") && user.EmailConfirmed == true)
                    users.Add(user);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        [Route("User/{username:regex(^user-)}")]
        [Culture]
        public ActionResult UserPage(string username, ManageMessageId? message)
        {
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie==null)
            {
                cookie = new HttpCookie("lang");
                cookie.Value = "ru";
            }

            string ChangePasswordSuccess = cookie.Value == "en" ? "Password changed success" : "Ваш пароль изменен.",
                AddPhotoSuccess = cookie.Value == "en" ? "Photo added success" : "Фото успешно добавлено.",
                EditProfileSuccess = cookie.Value == "en" ? "Profile edited success" : "Ваш профиль успешно изменён.",
                Error = cookie.Value == "en" ? "Error" : "Ошибка";

            ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess ? ChangePasswordSuccess
        : message == ManageMessageId.AddPhotoSuccess ? AddPhotoSuccess
        : message == ManageMessageId.EditProfileSuccess ? EditProfileSuccess
        : message == ManageMessageId.Error ? Error
        : "";

            UserPhotosViewModel UPmodel = new UserPhotosViewModel();
            string UserId = username.Split(new[] { "user-" }, StringSplitOptions.None).Last();

            ApplicationUser usr = db.Users.Where(item => item.Id == UserId).FirstOrDefault();

            if (usr == null)
            {
                ViewBag.ErrMsg = Resources.Resource.ErrorUserNotExist;
                return View("Error");
            }

            IEnumerable<Image> imgs = db.Images.Where(item => item.User.Id == usr.Id);
            UPmodel.user = usr;
            UPmodel.images = imgs;
            return View(UPmodel);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("user-" + User.Identity.GetUserId(), new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult Edit(ManageMessageId? message)
        {
            var Id = User.Identity.GetUserId();
            var userFindModel = db.Users.Where(p => p.Id == Id).FirstOrDefault();
            EditProfileViewModel profileInfo = new EditProfileViewModel();
            profileInfo.About = userFindModel.About;
            profileInfo.Avatar = userFindModel.Avatar;
            profileInfo.Email = userFindModel.Email;
            profileInfo.Name = userFindModel.Name;
            profileInfo.Surname = userFindModel.Surname;
            profileInfo.Tel = userFindModel.PhoneNumber;
            return View(profileInfo);
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProfileViewModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.PhoneNumber = model.Tel;
                    user.Email = model.Email;
                    user.About = model.About;
                    user.UserName = model.Email;
                    if (uploadImage != null)
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        }
                        user.Avatar = imageData;
                    }

                    var result = UserManager.Update(user);
                    AuthenticationManager.SignOut();
                    await SignInManager.SignInAsync(user, false, false);
                    UserManager.UpdateSecurityStamp(user.Id);
                    if (result.Succeeded)
                    {
                        var Id = User.Identity.GetUserId();
                        var userFindModel = db.Users.Where(p => p.Id == Id).FirstOrDefault();
                        return RedirectToAction("/user-" + User.Identity.GetUserId(), new { message = ManageMessageId.EditProfileSuccess });
                    }
                    AddErrors(result);
                    return View();
                }
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            EditProfileSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error,
            AddPhotoSuccess
        }

        #endregion
    }
}