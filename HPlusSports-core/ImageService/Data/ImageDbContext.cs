using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImageService;
using ImageService.Models;

namespace ImageService.Data
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext (DbContextOptions<ImageDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageService.Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
