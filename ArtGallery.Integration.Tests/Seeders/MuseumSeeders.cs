using ArtGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.Seeders {
    public class MuseumSeeders {
        public static async Task Seed(GalleryDbContext context) {
            Museum one = new() { Name = "Belladona", Slug = "museum-belladona", Country = "France" };
            Museum two = new() { Name = "Belladona", Slug = "museum-belladona", Country = "France" };
            Museum three = new() { Name = "Belladona", Slug = "museum-belladona", Country = "France" };
            List<Museum> museums = new List<Museum>() { one, two, three };
            await context.Museums.AddRangeAsync(museums);
            await context.SaveChangesAsync();
        }
    }
}
