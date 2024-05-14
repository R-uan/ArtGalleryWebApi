using ArtGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.Seeders {
    public class ArtworkSeeder {
        public static async Task Seed(GalleryDbContext context) {
            Artwork one = new Artwork() { Title = "Alisa", Description = "Mona", ImageURL = "url", Slug = "alisa-mona", ArtistId = 1, MuseumId = 1};
            Artwork two = new Artwork() { Title = "Scream", Description = "He screaming", ImageURL = "url", Slug = "aaa-aaah", ArtistId = 2, MuseumId = 2 };
            Artwork three = new Artwork() { Title = "Bent Clocks", Description = "It be bent", ImageURL = "url", Slug = "bent-clocks", ArtistId = 3, MuseumId = 3 };
            List<Artwork> artworks = new List<Artwork>() { one, two, three };
            await context.AddRangeAsync(artworks);
            await context.SaveChangesAsync();
        }
    }
}
