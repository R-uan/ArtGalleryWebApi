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
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            base.ConfigureWebHost(builder);
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
            builder.ConfigureTestServices(services => {
                services.RemoveAll(typeof(DbContextOptions<GalleryDbContext>));
                services.AddDbContext<GalleryDbContext>(db => {
                    db.UseNpgsql(configuration.GetConnectionString("TestDatabase"));
                });
                var db_context = GetDbContext(services);
                db_context.Database.EnsureDeleted();
            });
        }


        private static GalleryDbContext GetDbContext(IServiceCollection services) {
            var service_provider = services.BuildServiceProvider();
            var scope = service_provider.CreateScope();
            var db_context = scope.ServiceProvider.GetRequiredService<GalleryDbContext>();
            return db_context;
        }
    }
}