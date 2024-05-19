using ArtGallery.Models;
using ArtGallery.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Unit.Tests.Repositories {
    public class AdminRepositoryTest {
        private DbContextOptions<GalleryDbContext> CreateDbOptions() {
            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Unit.Test.json").Build();
            return new DbContextOptionsBuilder<GalleryDbContext>()
                .UseNpgsql(Configuration.GetConnectionString("TestDatabase"))
                .Options;
        }

        [OneTimeSetUp] 
        public async Task OneTimeSetUp() {
            var options = CreateDbOptions();
            using var context = new GalleryDbContext(options);
            await context.Database.EnsureCreatedAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown() {
            var options = CreateDbOptions();
            using var context = new GalleryDbContext(options);
            await context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task Get_AdminByUsername_ReturnsAdminEntity() {
            var options = CreateDbOptions();
            using (var context = new GalleryDbContext(options)) {
                var admin = new Admin() { Username = "username", Password = "password" };
                await context.Admin.AddAsync(admin);
                await context.SaveChangesAsync();
            }

            using (var context = new GalleryDbContext(options)) {
                var repository = new AdminRepository(context);
                var result = await repository.FindByUsername("username");
                Assert.That(result, Is.InstanceOf<Admin>());
            }
        }
    }
}
