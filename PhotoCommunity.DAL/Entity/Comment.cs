using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCommunity.DAL.Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual Image Image { get; set; }
    }
}
