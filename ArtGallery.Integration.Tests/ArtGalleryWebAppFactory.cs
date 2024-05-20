using ArtGallery.Integration.Tests.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests {
    public class ArtGalleryWebAppFactory : WebApplicationFactory<Program> {
        private GalleryDbContext? Db_context { set; get; }
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            base.ConfigureWebHost(builder);
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
            builder.ConfigureTestServices((services) => {
                services.RemoveAll(typeof(DbContextOptions<GalleryDbContext>));
                services.AddDbContext<GalleryDbContext>(db => {
                    db.UseNpgsql(configuration.GetConnectionString("TestDatabase"));
                });
                Db_context = GetDbContext(services);
            });
        }

        private static async Task DbSeeding(GalleryDbContext context) {
            await ArtistSeeder.Seed(context);
            await MuseumSeeder.Seed(context);
            await ArtworkSeeder.Seed(context);
            await AdminSeeder.Seed(context);
        }

        public async Task InitializeDatabase() {
            if (Db_context == null) throw new Exception("No database context provided;");
            await Db_context.Database.EnsureCreatedAsync();
            await DbSeeding(Db_context);
        }

        public async Task DestroyDatabase() {
            if (Db_context == null) throw new Exception("No database context provided;");
            await Db_context.Database.EnsureDeletedAsync();
        }

        private static GalleryDbContext GetDbContext(IServiceCollection services) {
            var service_provider = services.BuildServiceProvider();
            var scope = service_provider.CreateScope();
            var db_context = scope.ServiceProvider.GetRequiredService<GalleryDbContext>();
            return db_context;
        }
    }
}