using ArtGallery;
using FluentValidation;
using ArtGallery.Models;
using ArtGallery.Services;
using ArtGallery.Interfaces;
using ArtGallery.Repositories;
using ArtGallery.Utils.Validators;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery {
  public class Program {
    private static void Main(string[] args) {
      var builder = WebApplication.CreateBuilder(args);
      var Configuration = builder.Configuration;

      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddControllers();
      builder.Services.AddSwaggerGen(c => {
        c.ResolveConflictingActions(x => x.First());
      });

      builder.Services.Configure<RouteOptions>(options => {
        options.ConstraintMap.Add("string", typeof(string));
      });

      builder.Services.AddDbContext<GalleryDbContext>(options => {
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
      });

      /* Artist */
      builder.Services.AddScoped<IValidator<Artist>, ArtistValidator>();
      builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
      builder.Services.AddScoped<IArtistService, ArtistService>();

      /* Museum */
      builder.Services.AddScoped<IValidator<Museum>, MuseumValidator>();
      builder.Services.AddScoped<IMuseumRepository, MuseumRepository>();
      builder.Services.AddScoped<IMuseumService, MuseumService>();

      /* Artwork */
      builder.Services.AddScoped<IValidator<Artwork>, ArtworkValidator>();
      builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
      builder.Services.AddScoped<IArtworkService, ArtworkService>();

      var app = builder.Build();
      app.MapControllers();

      if (app.Environment.IsDevelopment()) {
        app.UseSwagger();
        app.UseSwaggerUI();

      }

      app.Run();
    }
  }
}