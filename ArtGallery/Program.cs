using System.Text;
using ArtGallery.DTO;
using FluentValidation;
using ArtGallery.Utils;
using ArtGallery.Models;
using StackExchange.Redis;
using ArtGallery.Components;
using ArtGallery.Utils.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ArtGallery.Interfaces.Services;
using ArtGallery.Application.Services;
using ArtGallery.Interfaces.Repository;
using ArtGallery.Application.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ArtGallery
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);
            var Configuration = Builder.Configuration;
            var JwtSettings = Configuration.GetSection("Jwt").Get<JWTSettings>();

            //
            // Cross Origin Resource Sharing Policies
            Builder.Services.AddCors(options => options.AddPolicy(name: "AllowAll", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }));


            Builder.Services.AddControllers();
            Builder.Services.AddScoped<JWTHelper>();
            Builder.Services.AddEndpointsApiExplorer();
            Builder.Services.Configure<JWTSettings>(Configuration.GetSection("Jwt"));
            Builder.Services.AddSwaggerGen(c => { c.ResolveConflictingActions(x => x.First()); });
            Builder.Services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("string", typeof(string));
            });

            //
            // Database Cofiguration
            //
            Builder.Services.AddDbContext<GalleryDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            /*
			* JSON Web Token authentication middleware
			*/
            Builder.Services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = JwtSettings!.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings!.SecretKey)),
                };
            });

            Builder.Services.AddAuthorization();

            Builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("127.0.0.1"));
            Builder.Services.AddScoped<IRedisRepository, RedisRepository>();

            //
            // Admin dependencies.
            Builder.Services.AddScoped<IValidator<Admin>, AdminValidator>();
            Builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            Builder.Services.AddScoped<IAdminService, AdminService>();
            //
            // Artist dependencies.
            Builder.Services.AddScoped<IValidator<ArtistDTO>, ArtistValidator>();
            Builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            Builder.Services.AddScoped<IArtistService, ArtistService>();
            //
            // Museum dependencies.
            Builder.Services.AddScoped<IValidator<MuseumDTO>, MuseumValidator>();
            Builder.Services.AddScoped<IMuseumRepository, MuseumRepository>();
            Builder.Services.AddScoped<IMuseumService, MuseumService>();
            //
            // Artwork dependencies.
            Builder.Services.AddScoped<IValidator<ArtworkDTO>, ArtworkValidator>();
            Builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
            Builder.Services.AddScoped<IArtworkService, ArtworkService>();
            //
            // Period dependencies.
            Builder.Services.AddScoped<IValidator<PeriodDTO>, PeriodValidator>();
            Builder.Services.AddScoped<IPeriodRepository, PeriodRepository>();
            Builder.Services.AddScoped<IPeriodService, PeriodService>();

            Builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            Builder.Services.AddScoped<IDashboardService, DashboardService>();

            //
            //	Blazor Stuff
            Builder.Services.AddRazorComponents().AddInteractiveServerComponents();

            var app = Builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();
            app.MapControllers();
            app.UseStaticFiles();

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.UseCors("AllowAll");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.Run();
        }
    }
}