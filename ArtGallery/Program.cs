namespace ArtGallery;

using System.Text;
using FluentValidation;
using ArtGallery.Models;
using ArtGallery.Services;
using ArtGallery.Interfaces;
using ArtGallery.Repositories;
using ArtGallery.Utils.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ArtGallery.Utils;

public class Program {
	private static void Main(string[] args) {
		var Builder = WebApplication.CreateBuilder(args);
		var Configuration = Builder.Configuration;
		var JwtSettings = Configuration.GetSection("Jwt").Get<JWTSettings>();

		/*
		*	Cross Origin Resource Sharing Policies
		*/
		Builder.Services.AddCors(options => options.AddPolicy(name: "AllowAll", policy => {
			policy.AllowAnyOrigin();
			policy.AllowAnyHeader();
			policy.AllowAnyMethod();
		}));


		Builder.Services.AddControllers();
		Builder.Services.AddScoped<AuthHelper>();
		Builder.Services.AddEndpointsApiExplorer();
		Builder.Services.Configure<JWTSettings>(Configuration.GetSection("Jwt"));
		Builder.Services.AddSwaggerGen(c => { c.ResolveConflictingActions(x => x.First()); });

		Builder.Services.Configure<RouteOptions>(options => {
			options.ConstraintMap.Add("string", typeof(string));
		});

		/*
		* Database Cofiguration
		*/
		Builder.Services.AddDbContext<GalleryDbContext>(options => {
			options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
		});

		/*
		* JSON Web Token authentication middleware
		*/
		Builder.Services.AddAuthentication(cfg => {
			cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(x => {
			x.SaveToken = true;
			x.RequireHttpsMetadata = false;
			x.TokenValidationParameters = new TokenValidationParameters {
				ValidateIssuer = true,
				ValidateAudience = false,
				ValidIssuer = JwtSettings!.Issuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings!.SecretKey)),
			};
		});
		Builder.Services.AddAuthorization();

		/* Admin */
		Builder.Services.AddScoped<IValidator<Admin>, AdminValidator>();
		Builder.Services.AddScoped<IAdminRepository, AdminRepository>();
		Builder.Services.AddScoped<IAdminService, AdminService>();

		/* Artist */
		Builder.Services.AddScoped<IValidator<Artist>, ArtistValidator>();
		Builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
		Builder.Services.AddScoped<IArtistService, ArtistService>();

		/* Museum */
		Builder.Services.AddScoped<IValidator<Museum>, MuseumValidator>();
		Builder.Services.AddScoped<IMuseumRepository, MuseumRepository>();
		Builder.Services.AddScoped<IMuseumService, MuseumService>();

		/* Artwork */
		Builder.Services.AddScoped<IValidator<Artwork>, ArtworkValidator>();
		Builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
		Builder.Services.AddScoped<IArtworkService, ArtworkService>();

		var app = Builder.Build();

		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		app.UseCors("AllowAll");
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();

		}

		app.Run();
	}
}
