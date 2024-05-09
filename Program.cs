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
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options => {
	options.ConstraintMap.Add("string", typeof(string));
});

builder.Services.AddDbContext<GalleryDbContext>(options => {
	options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IValidator<Artist>, ArtistValidator>();
builder.Services.AddScoped<IArtistService, ArtistService>();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();

}

app.Run();
