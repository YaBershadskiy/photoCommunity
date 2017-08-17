using PagedList;
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
        [Display(Name = "PhotoName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ChosePhoto", ResourceType = typeof(Resources.Resource))]
        public HttpPostedFileBase Img { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resource))]
        public string Description { get; set; }
    }

    public class ListPhotoViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM, hh:mm}")]
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public byte[] Img { get; set; }

        public ApplicationUser Owner { get; set; }

        public int Rate { get; set; }

        [Display(Name = "AddComment",ResourceType = typeof(Resources.Resource))]
        public List<Comment> Comments { get; set; }

        public string Description { get; set; }
    }

   public class UserPhotosViewModel
    {
        public IPagedList<Image> images { get; set; }
        public ApplicationUser user { get; set; }
    }
}