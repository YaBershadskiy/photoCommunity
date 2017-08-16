using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoCommunity.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Image Image { get; set; }
    }

    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Img { get; set; }

        [DefaultValue(0)]
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Image()
        {
            Comments = new List<Comment>();
        }
    }

    public class Like
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Image Image { get; set; }
    }

}