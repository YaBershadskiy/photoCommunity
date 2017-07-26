using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoCommunity.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhotoCommunity.Controllers
{
    public class PhotoController : Controller
    {
        ApplicationDbContext AppCtx = new ApplicationDbContext();

        // GET: Photo
        public ActionResult Index()
        {
            List<ListPhotoViewModel> photoInfo = new List<ListPhotoViewModel>();
            List<Image> images = AppCtx.Images.OrderByDescending(item => item.Date).ToList<Image>();
            foreach (var item in images)
            {
                ListPhotoViewModel photoModel = new ListPhotoViewModel();
                photoModel.Id = item.Id;
                photoModel.Rate = item.Rate;
                photoModel.Name = item.Name;
                photoModel.Img = item.Img;
                photoModel.Date = item.Date;
                photoModel.Owner = item.User;
                photoInfo.Add(photoModel);
            }
            return View(photoInfo);
        }

        [Authorize]
        public ActionResult Add()
        {
            return View();
        }       

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Add(AddPhotoViewModel model)
        {
            ///TODO!!!После закгрузки картинки выводить сообщение и очищать поля ввода
            byte[] imageData = null;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(model.Img.InputStream))
            {
                imageData = binaryReader.ReadBytes(model.Img.ContentLength);
            }
            string userId = User.Identity.GetUserId();
            ApplicationUser user = AppCtx.Users.Where(item => item.Id == userId).FirstOrDefault();
            var photo = new Image { Name = model.Name, Date = DateTime.Now, User = user, Img = imageData, Description = model.Description };
            AppCtx.Images.Add(photo);
            await AppCtx.SaveChangesAsync();
            return View(model);
        }

        [Route("Photo/{id:int}")]
        public ActionResult Show(int id)
        {
            ///TODO!! 
            ///- проверку на не указанный Ид (типа если вручную вызвали шоу) и если такой картинки не существует
            Image img = AppCtx.Images.Where(o => o.Id == id).FirstOrDefault();
            if (img == null)
            {
                return View("Error");
            }
            ListPhotoViewModel photoInfo = new ListPhotoViewModel();
            photoInfo.Id = img.Id;
            photoInfo.Name = img.Name;
            photoInfo.Img = img.Img;
            photoInfo.Rate = img.Rate;
            photoInfo.Comments = img.Comments.ToList<Comment>();
            photoInfo.Owner = img.User;
            return View(photoInfo);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(string comment, int imgId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ErrMsg = "Комментарий оставить может только авторизованный пользователь";
                return PartialView("test_error");
            }

            Image img = AppCtx.Images.Where(o => o.Id == imgId).FirstOrDefault();
            var userId = User.Identity.GetUserId();

            if (comment != "" && comment != null)
            {
                Comment newComment = new Comment();
                newComment.Text = comment;
                newComment.Username = AppCtx.Users.Where(o => o.Id == userId).FirstOrDefault().Name;
                newComment.Image = img;
                newComment.Date = DateTime.Now;
                AppCtx.Comments.Add(newComment);
                await AppCtx.SaveChangesAsync();

                img = AppCtx.Images.Where(o => o.Id == imgId).FirstOrDefault();
            }
            return PartialView(img.Comments.OrderBy(item => item.Date));
        }

        public ActionResult Like(string like, string unlike, int imgId)
        {
            Image img = AppCtx.Images.Where(o => o.Id == imgId).FirstOrDefault();

            if (img == null)
            {
                return View("Error");
            }

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ErrMsg = "Авторизуйтесь!";
                return PartialView("test_error");
            }

            string userId = User.Identity.GetUserId();

            ApplicationUser user = AppCtx.Users.Where(item => item.Id == userId).FirstOrDefault();
            int likes = AppCtx.Likes.Where(item => item.Image.Id == img.Id && item.User.Id == user.Id).Count();
            if (likes == 0)
            {
                Like newLike = new Like();
                newLike.Image = img;
                newLike.User = user;
                AppCtx.Likes.Add(newLike);

                if (like != null)
                {
                    img.Rate++;
                }
                if (unlike != null)
                {
                    img.Rate--;
                }
                AppCtx.SaveChanges();
            }
            else
            {
                ViewBag.ErrMsg = "Оценить можно только 1 раз!";
                return PartialView("test_error");
            }

            return PartialView(img);
        }

    }
}