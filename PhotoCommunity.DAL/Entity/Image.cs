using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhotoCommunity.DAL.Entity
{
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

        public virtual string UserId { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Image()
        {
            Comments = new List<Comment>();
        }
    }
}
