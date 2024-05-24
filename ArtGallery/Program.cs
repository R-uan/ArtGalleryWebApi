using ArtGallery;
using System.Text;
using ArtGallery.Utils;
using FluentValidation;
using ArtGallery.Models;
using ArtGallery.Services;
using ArtGallery.Interfaces;
using ArtGallery.Repositories;
using ArtGallery.Utils.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ArtGallery {
    public class Program {
        private static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder.Configuration;

            builder.Services.AddCors(options => options.AddPolicy(name: "AllowAll", policy => {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c => { c.ResolveConflictingActions(x => x.First()); });

            builder.Services.Configure<RouteOptions>(options => {
                options.ConstraintMap.Add("string", typeof(string));
            });

            builder.Services.AddDbContext<GalleryDbContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x => {
                x.SaveToken = false;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-segredo-mega-super-grande-haha")),
                };
            });

            /* Admin */
            builder.Services.AddScoped<IValidator<Admin>, AdminValidator>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();
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
            app.UseAuthentication();
            app.MapControllers();
            app.UseCors("AllowAll");
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.Run();
        }
    }
}