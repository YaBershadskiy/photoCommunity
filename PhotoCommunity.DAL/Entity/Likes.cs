using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCommunity.DAL.Entity
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public string User_id { get; set; }

        public virtual Image Image { get; set; }
    }
}
