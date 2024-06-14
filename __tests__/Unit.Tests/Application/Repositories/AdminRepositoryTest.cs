using ArtGallery;
using ArtGallery.Controllers;
using ArtGallery.Models;
using ArtGallery.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Unit.Tests.Repositories
{
	[TestFixture]
	public class AdminRepositoryTest
	{
		private DbContextOptions<GalleryDbContext> GetDatabaseOptions()
		{
			return new DbContextOptionsBuilder<GalleryDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;
		}

		[Test]
		public async Task Save_An_Admin_Test()
		{
			var options = GetDatabaseOptions();
			using (var context = new GalleryDbContext(options))
			{
				AdminRepository repository = new(context);
				Admin admin = new() { Username = "Hi", Password = "World" };
				var save = await repository.AddOneAdmin(admin);
				Assert.That(save, Is.Not.Null);
				Assert.That(save, Is.EqualTo(1));
			}
		}

		[Test]
		public async Task Find_Admin_By_Username_Test()
		{
			var options = GetDatabaseOptions();
			using (var context = new GalleryDbContext(options))
			{
				//
				// Creates the admin to be found
				Admin admin = new() { Username = "Hi", Password = "World" };
				await context.Admin.AddAsync(admin);
				await context.SaveChangesAsync();
				//
				// Finds the admin by username
				AdminRepository repository = new(context);
				var find = await repository.FindByUsername(admin.Username);
				Assert.That(find, Is.Not.Null);
				Assert.That(find, Is.TypeOf<Admin>());
			}
		}

		[Test]
		public async Task Find_A_Non_Existent_Username_Test()
		{
			var options = GetDatabaseOptions();
			using (var context = new GalleryDbContext(options))
			{
				AdminRepository repository = new(context);
				var find = await repository.FindByUsername("does-not-exist");
				Assert.That(find, Is.Null);
			}
		}
	}
}
