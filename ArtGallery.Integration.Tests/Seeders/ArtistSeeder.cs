using ArtGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.Seeders {
    public class ArtistSeeder {
        public static async Task Seed(GalleryDbContext context) {
            Artist one = new Artist() { Name = "Leonardo", Slug = "slug-leonardo" };
            Artist two = new Artist() { Name = "Donatelo", Slug = "slug-donatelo" };
            Artist three = new Artist() { Name = "Michaelangelo", Slug = "slug-michael" };
            await context.AddRangeAsync(one, two, three);
            await context.SaveChangesAsync();
        }
    }
}