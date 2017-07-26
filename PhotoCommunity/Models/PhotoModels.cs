using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoCommunity.Models
{
    public class AddPhotoViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Название фото")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Выбирите фото")]
        public HttpPostedFileBase Img { get; set; }

        [Display(Name = "Описание фото")]
        public string Description { get; set; }
    }

    public class ListPhotoViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public byte[] Img { get; set; }

        public ApplicationUser Owner { get; set; }

        public int Rate { get; set; }

        [Display(Name = "Добавить комментарий")]
        public List<Comment> Comments { get; set; }
    }

    public class CommentsViewModel
    {
        [Display(Name = "Комментарии")]
        public ICollection<Comment> Comments { get; set; }
    }

    public class UserPhotosViewModel
    {
        public IEnumerable<Image> images { get; set; }
        public ApplicationUser user { get; set; }
    }
}