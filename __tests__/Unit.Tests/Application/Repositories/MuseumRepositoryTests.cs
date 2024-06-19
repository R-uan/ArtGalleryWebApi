using ArtGallery;
using ArtGallery.DTO;
using ArtGallery.Utils;
using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Application.Repositories;

namespace Unit.Tests.Repositories
{
    [TestFixture]
    public class MuseumRepositoryTests
    {
        private readonly Museum test_entity = new Museum()
        {
            MuseumId = 1,
            City = "Paris",
            State = null,
            Latitude = 48.8606f,
            Longitude = 2.3376f,
            Inauguration = 1793,
            Slug = "louvre-museum",
            Name = "Louvre Museum",
            Country = "France",
        };

        private DbContextOptions<GalleryDbContext> GetDatabaseOptions()
        {
            return new DbContextOptionsBuilder<GalleryDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
        }

        [Test, Order(1)]
        public async Task FindAllTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                MuseumRepository repository = new(context);
                var find = await repository.Find();
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<List<Museum>>());
            }
        }

        [Test, Order(2)]
        public async Task FindAllPartialTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                MuseumRepository repository = new(context);
                var find = await repository.FindPartial();
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<List<PartialMuseumDTO>>());
            }
        }

        [Test, Order(3)]
        public async Task FindOneByIdTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                //
                //  Add entity to the database.
                await context.AddAsync<Museum>(test_entity);
                await context.SaveChangesAsync();
                //
                //  Find entity.
                MuseumRepository repository = new(context);
                var find = await repository.FindById(this.test_entity.MuseumId);
                //
                //  Assertions
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<Museum>());
            }
        }

        [Test, Order(4)]
        public async Task FindOneBySlugTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                await context.AddAsync<Museum>(test_entity);
                await context.SaveChangesAsync();
                MuseumRepository repository = new(context);
                var find = await repository.FindBySlug(this.test_entity.Slug);
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<Museum>());
            }
        }

        [Test, Order(5)]
        public async Task FindPaginatedTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                MuseumRepository repository = new(context);
                var find = await repository.FindPartialPaginated(1);
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<PaginatedResponse<PartialMuseumDTO>>());
            }
        }

        [Test, Order(6)]
        public async Task PaginatedQueryTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                MuseumRepository repository = new(context);
                MuseumQueryParams param = new() { Name = "leo" };
                var query = await repository.PaginatedQuery(param, 1);
                Assert.That(query, Is.Not.Null);
                Assert.That(query, Is.TypeOf<PaginatedResponse<PartialMuseumDTO>>());

            }
        }

        [Test, Order(7)]
        public async Task SaveOneTest()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                MuseumRepository repository = new(context);
                Museum museum = new() { Country = "USA", Name = "Museum Dois", Slug = "museum-dois" };
                var save = await repository.Save(museum);
                Assert.That(save, Is.TypeOf<Museum>());
            }
        }

        [Test, Order(8)]
        public async Task UpdateOneTest()
        {
            var options = GetDatabaseOptions();

            using (var context = new GalleryDbContext(options))
            {
                //
                // Create the museum to update.
                MuseumRepository repository = new(context);
                Museum museum = new() { Country = "USA", Name = "Museum Dois", Slug = "museum-dois" };
                var save = await context.AddAsync(museum);
                await context.SaveChangesAsync();
                //
                // Update the museum.
                UpdateMuseumDTO update_museum = new() { Name = "Lucy" };
                var update = await repository.UpdateById(save.Entity.MuseumId, update_museum);
                Assert.That(update, Is.TypeOf<Museum>());
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
                Museum museum = new() { Country = "USA", Name = "Rebecca", Slug = "re-bb-ecca" };
                var save = await context.Museums.AddAsync(museum);
                await context.SaveChangesAsync();
                //
                // Check if the entity was created.
                MuseumRepository repository = new(context);
                var check = await context.Museums.FindAsync(save.Entity.MuseumId);
                Assert.That(check, Is.Not.Null);
                Assert.That(check.Name, Is.EqualTo(museum.Name));
                //
                // Deletes the entity
                var delete = await repository.DeleteById(save.Entity.MuseumId);
                Assert.That(delete, Is.True);
            }
        }
    }
}