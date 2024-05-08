using ArtGallery;
using ArtGallery.Models;
using ArtGallery.Repositories;
using ArtGallery.Utils.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

/* Artists */
builder.Services.AddScoped<IValidator<Artist>, ArtistValidator>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IArtistService, ArtistService>();


builder.Services.AddDbContext<GalleryDbContext>(options => {
	options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.MapControllers();
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/", async context => {
	try {
		using (var scope = app.Services.CreateScope()) {
			var dbContext = scope.ServiceProvider.GetRequiredService<GalleryDbContext>();
			await dbContext.Database.OpenConnectionAsync();
			await context.Response.WriteAsync($"Connection to PostgreSQL database successful!");
		}
	} catch (Exception ex) {
		await context.Response.WriteAsync($"Error: {ex.Message}");
	}
});



app.Run();
