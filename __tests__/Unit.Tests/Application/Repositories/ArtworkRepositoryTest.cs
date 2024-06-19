using ArtGallery;
using ArtGallery.DTO;
using ArtGallery.Utils;
using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Application.Repositories;

namespace Unit.Tests.Repositories
{
    [TestFixture]
    public class ArtworkRepositoryTest
    {
        private readonly Artwork test_entity = new Artwork() { ArtworkId = 1, Title = "Mona", Slug = "mona", History = "", ImageURL = "" };

        private DbContextOptions<GalleryDbContext> GetDatabaseOptions()
        {
            return new DbContextOptionsBuilder<GalleryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Test, Order(1)]
        public async Task Find_All__Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtworkRepository repository = new(context);
                var find = await repository.Find();
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<List<Artwork>>());
            }
        }

        [Test, Order(2)]
        public async Task Find_All_Partial_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtworkRepository repository = new(context);
                var find = await repository.FindPartial();
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<List<PartialArtworkDTO>>());
            }
        }

        [Test, Order(3)]
        public async Task Find_All_Partial_Paginated_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtworkRepository repository = new(context);
                var find = await repository.FindPartialPaginated(1);
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<PaginatedResponse<PartialArtworkDTO>>());
            }
        }

        [Test, Order(4)]
        public async Task Find_Query_Paginated_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtworkRepository repository = new(context);
                ArtworkQueryParams param = new() { Title = "mona" };
                var query = await repository.PaginatedQuery(param, 1);
                Assert.That(query, Is.TypeOf<PaginatedResponse<PartialArtworkDTO>>());
            }
        }

        [Test, Order(5)]
        public async Task Find_By_Id_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                //
                //  Add artwork to the database.
                await context.AddAsync<Artwork>(this.test_entity);
                await context.SaveChangesAsync();
                //
                //  Find artwork.
                ArtworkRepository repository = new(context);
                var find = await repository.FindById(this.test_entity.ArtworkId);
                //
                //  Assertions
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<Artwork>());
            }
        }

        [Test, Order(6)]
        public async Task Find_By_Slug_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                //
                //  Add artwork to the database.
                await context.AddAsync<Artwork>(this.test_entity);
                await context.SaveChangesAsync();
                //
                //  Find artwork.
                ArtworkRepository repository = new(context);
                var find = await repository.FindBySlug(this.test_entity.Slug);
                //
                //  Assertions
                Assert.That(find, Is.Not.Null);
                Assert.That(find, Is.TypeOf<Artwork>());
            }
        }

        [Test, Order(7)]
        public async Task Save_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                ArtworkRepository repository = new(context);
                Artwork artwork = new() { Title = "Scream", Slug = "icecream", History = "", ImageURL = "" };
                var save = await repository.Save(artwork);
                Assert.That(save, Is.Not.Null);
                Assert.That(save, Is.TypeOf<Artwork>());
            }
        }

        [Test, Order(8)]
        public async Task Delete_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                //
                //  Create the entity to be deleted.
                Artwork artwork = new() { Title = "Scream", Slug = "icecream", History = "", ImageURL = "" };
                var save = await context.Artworks.AddAsync(artwork);
                await context.SaveChangesAsync();
                //
                //  Delete
                ArtworkRepository repository = new(context);
                var delete = await repository.DeleteById(save.Entity.ArtworkId);
                Assert.That(delete, Is.True);
            }
        }

        [Test, Order(9)]
        public async Task Update_Test()
        {
            var options = GetDatabaseOptions();
            using (var context = new GalleryDbContext(options))
            {
                //
                // Create entity to update.
                Artwork artwork = new() { Title = "Scream", Slug = "icecream", History = "", ImageURL = "" };
                var save = await context.Artworks.AddAsync(artwork);
                await context.SaveChangesAsync();
                //
                // Update
                ArtworkRepository repository = new(context);
                var update_entity = new UpdateArtworkDTO() { Title = "sadge" };
                var update = await repository.UpdateById(save.Entity.ArtworkId, update_entity);
                //
                //  Assertions
                Assert.That(update, Is.Not.Null);
                Assert.That(update.Title, Is.EqualTo(update_entity.Title));
            }
        }
    }
}