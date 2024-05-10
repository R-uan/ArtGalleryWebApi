using ArtGallery;
using FluentValidation;
using ArtGallery.Models;
using ArtGallery.Services;
using ArtGallery.Repositories;
using ArtGallery.Utils.Validators;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Controllers;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options => {
	options.ConstraintMap.Add("string", typeof(string));
});

builder.Services.AddDbContext<GalleryDbContext>(options => {
	options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
});

/* Artist */
builder.Services.AddScoped<IRepository<Artist, UpdateArtist, ArtistPartial>, ArtistRepository>();
builder.Services.AddScoped<IService<Artist, UpdateArtist, ArtistPartial>, ArtistService>();
builder.Services.AddScoped<IValidator<Artist>, ArtistValidator>();

/* Museum */
builder.Services.AddScoped<IRepository<Museum, UpdateMuseum, MuseumPartial>, MuseumRepository>();
builder.Services.AddScoped<IService<Museum, UpdateMuseum, MuseumPartial>, MuseumService>();
builder.Services.AddScoped<IValidator<Museum>, MuseumValidator>();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();

}

app.Run();
