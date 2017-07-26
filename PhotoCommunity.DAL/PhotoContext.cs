using PhotoCommunity.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCommunity.DAL
{
    public class PhotoContext : DbContext
    {
        public PhotoContext() : base("DefaultConnection")
        {
        }

       public DbSet<Image> Images { get; set; }
       public DbSet<Comment> Comments { get; set; }
       public DbSet<Like> Likes { get; set; }

    }
}
