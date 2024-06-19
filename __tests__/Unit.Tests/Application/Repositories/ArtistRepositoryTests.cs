using ArtGallery;
using ArtGallery.DTO;
using ArtGallery.Utils;
using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Application.Repositories;

namespace Unit.Tests.Repositories
{
    [TestFixture]
    public class ArtistRepositoryTests
    {
        private readonly Artist test_entity = new()
        {
            ArtistId = 1,
            Name = "Leonardo Da Vinci",
            Country = "Italy",
            Slug = "leonardo-da-vinci",
            ImageURL = "https://p2.trrsf.com/image/fget/cf/774/0/images.terra.com/2019/06/24/leo.jpg",
            Profession = "Polymath",
            Movement = "Renaissence",
        };

        private DbContextOptions<GalleryDbContext> GetDatabaseOptions()
        {
            return new DbContextOptionsBuilder<GalleryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Test, Order(1)]
        public async Task FindAllArtistsTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtistRepository repository = new(context);
                var find = await repository.Find();
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<List<Artist>>());
            }
        }

        [Test, Order(2)]
        public async Task FindAllPartialArtistTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtistRepository repository = new(context);
                var find = await repository.FindPartial();
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<List<PartialArtistDTO>>());
            }
        }

        [Test, Order(3)]
        public async Task FindOneByIdTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                await context.AddAsync<Artist>(test_entity);
                await context.SaveChangesAsync();
                ArtistRepository repository = new(context);
                var find = await repository.FindById(this.test_entity.ArtistId);
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<Artist>());
            }
        }

        [Test, Order(4)]
        public async Task FindOneBySlugTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                await context.AddAsync<Artist>(test_entity);
                await context.SaveChangesAsync();
                ArtistRepository repository = new(context);
                var find = await repository.FindBySlug(this.test_entity.Slug);
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<Artist>());
            }
        }

        [Test, Order(5)]
        public async Task FindPaginatedTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtistRepository repository = new(context);
                var find = await repository.FindPartialPaginated(1);
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<PaginatedResponse<PartialArtistDTO>>());
            }
        }

        [Test, Order(6)]
        public async Task PaginatedQueryTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtistRepository repository = new(context);
                ArtistQueryParams param = new() { Name = "leo" };
                var query = await repository.PaginatedQuery(param, 1);
                Assert.That(query, Is.Not.Null);
                Assert.That(query, Is.TypeOf<PaginatedResponse<PartialArtistDTO>>());

            }
        }

        [Test, Order(7)]
        public async Task SaveOneTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtistRepository repository = new(context);
                Artist artist = new() { Country = "USA", Name = "David", ImageURL = "none", Slug = "da-vid" };
                var save = await repository.Save(artist);
                Assert.That(save, Is.TypeOf<Artist>());
            }
        }

        [Test, Order(8)]
        public async Task UpdateOneTest()
        {
            var options = GetDatabaseOptions();

            using (var context = new GalleryDbContext(options))
            {
                //
                // Create the artist to update.
                ArtistRepository repository = new(context);
                Artist artist = new() { Country = "USA", Name = "David", ImageURL = "none", Slug = "da-vid" };
                var save = await context.AddAsync(artist);
                await context.SaveChangesAsync();
                //
                // Update the artist.
                UpdateArtistDTO update_artist = new() { Name = "Lucy" };
                var update = await repository.UpdateById(save.Entity.ArtistId, update_artist);
                Assert.That(update, Is.TypeOf<Artist>());
                Assert.That(update.Name, Is.EqualTo("Lucy"));
            }
        }

        [Test, Order(9)]
        public async Task DeleteOneTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                //
                // Create the entity to be deleted.
                Artist artist = new() { Country = "USA", Name = "Rebecca", ImageURL = "none", Slug = "re-bb-ecca" };
                var save = await context.Artists.AddAsync(artist);
                await context.SaveChangesAsync();
                //
                // Check if the entity was created.
                ArtistRepository repository = new(context);
                var check = await context.Artists.FindAsync(save.Entity.ArtistId);
                Assert.That(check, Is.Not.Null);
                Assert.That(check.Name, Is.EqualTo(artist.Name));
                //
                // Deletes the entity
                var delete = await repository.DeleteById(save.Entity.ArtistId);
                Assert.That(delete, Is.True);
            }
        }
    }
}
