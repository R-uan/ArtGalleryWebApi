using ArtGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.Seeders {
    public class MuseumSeeder {
        public static async Task Seed(GalleryDbContext context) {
            Museum one = new() { Name = "Belladona", Slug = "museum-belladona", Country = "France" };
            Museum two = new() { Name = "Louvre", Slug = "museum-louvre", Country = "France" };
            Museum three = new() { Name = "Caxias", Slug = "museum-caxias", Country = "Brazil" };
            List<Museum> museums = new List<Museum>() { one, two, three };
            await context.Museums.AddRangeAsync(museums);
            await context.SaveChangesAsync();
        }
    }
}
