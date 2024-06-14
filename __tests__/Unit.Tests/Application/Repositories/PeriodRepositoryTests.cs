using ArtGallery;
using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Unit.Tests.Repositories
{
    [TestFixture]
    public class PeriodRepositoryTests
    {

        private DbContextOptions<GalleryDbContext> GetInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<GalleryDbContext>()
            .UseInMemoryDatabase(databaseName: "period-repository-unit-test")
            .Options;
        }

        [Test, Order(1)]
        public async Task FindAllPartialTest()
        {
            var options = GetInMemoryDatabase();
            using (var context = new GalleryDbContext(options))
            {
                PeriodRepository repository = new PeriodRepository(context);
                var partialList = await repository.FindPartial();
                Assert.That(partialList, Is.TypeOf<List<PartialPeriod>>());
            }
        }

        [Test, Order(2)]
        public async Task SaveAPeriodTest()
        {
            var options = GetInMemoryDatabase();
            using (var context = new GalleryDbContext(options))
            {
                PeriodRepository repository = new(context);
                Period period = new() { Name = "Period", Summary = "Smary" };
                var save = await repository.Save(period);
                Assert.That(save, Is.Not.Null);
                Assert.That(save.PeriodId, Is.EqualTo(1));
            }
        }

        [Test, Order(3)]
        public async Task FindOneByIdTest()
        {
            int id;
            var options = GetInMemoryDatabase();
            Period period = new() { Name = "Period", Summary = "Smary" };

            using (var context = new GalleryDbContext(options))
            {
                PeriodRepository repository = new(context);
                var save = await repository.Save(period);
                Assert.That(save, Is.Not.Null);
                id = save.PeriodId;
            }

            using (var context = new GalleryDbContext(options))
            {
                PeriodRepository repository = new(context);
                var find = await repository.FindById(id);
                Assert.That(find, Is.Not.Null);
                Assert.That(find.Name, Is.EqualTo(period.Name));
            }
        }

        [Test, Order(4)]
        public async Task DeleteByIdTest()
        {
            var options = GetInMemoryDatabase();
            using (var context = new GalleryDbContext(options))
            {
                PeriodRepository repository = new(context);
                var delete = await repository.Delete(1);
                Assert.That(delete, Is.EqualTo(true));
            }
        }

        [Test, Order(5)]
        public async Task FindAllTest()
        {
            var options = GetInMemoryDatabase();
            using (var context = new GalleryDbContext(options))
            {
                var repository = new PeriodRepository(context);
                var list = await repository.Find();
                Assert.That(list, Is.TypeOf<List<Period>>());
            }
        }
    }
}