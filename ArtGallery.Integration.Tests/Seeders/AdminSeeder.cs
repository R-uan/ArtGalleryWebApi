using ArtGallery.Models;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.Seeders {
    public class AdminSeeder {
        public static async Task Seed(GalleryDbContext context) {
            var password = BCrypt.Net.BCrypt.HashPassword("12345678");
            Admin one = new() { Username = "admin", Password = password };
            await context.AddAsync(one);
            await context.SaveChangesAsync();
        }
    }
}
