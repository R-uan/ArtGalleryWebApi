using ArtGallery;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryUnitTests.Application.Repository;
[TestFixture]
public class PeriodRepositoryTests
{

	private DbContextOptions<GalleryDbContext> GetInMemoryDatabase()
	{
		return new DbContextOptionsBuilder<GalleryDbContext>()
		.UseInMemoryDatabase(databaseName: "period-repository-unit-test")
		.Options;
	}

	[Test]
	public void InMemoryDatabaseConnectionTest()
	{
		var options = GetInMemoryDatabase();
		using (var context = new GalleryDbContext(options))
		{
			var list = context.Periods.ToList();
			Assert.That(list, Is.Not.Null);
		}
	}
}