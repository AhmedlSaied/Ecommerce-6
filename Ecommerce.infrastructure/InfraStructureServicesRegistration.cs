using Ecommerce.Application.contracts;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Contracts;
using Ecommerce.infrastructure.Data;
using Ecommerce.infrastructure.Identity.Data;
using Ecommerce.infrastructure.Identity.Entity;
using Ecommerce.infrastructure.Identity.JwtTokenCreator;
using Ecommerce.infrastructure.Inspectors;
using Ecommerce.infrastructure.Repostory;
using Ecommerce.infrastructure.seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
namespace Ecommerce.infrastructure
{
    public static class InfraStructureServicesRegistration
    {
        public static IServiceCollection AddInFrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(opetion => opetion.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).AddInterceptors(new SoftDeleteInspector()));
            services.AddDbContext<DbIdentityContext>(option => option.UseSqlServer(configuration.GetConnectionString("IdentyConnection")));
            services.AddKeyedScoped<IDataSeeder, CataLogDataSeeder>("CataLog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            services.AddScoped<IUniteWork, UniteOfWork>();
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DbIdentityContext>();
            services.AddSingleton<IConnectionMultiplexer>
                (
                 Config =>
                 {
                     return ConnectionMultiplexer.Connect(configuration: configuration.GetConnectionString("RedisConnetion")!);
                 }
                );
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IBasketItem, BasketITem>();
            services.AddScoped<IIdentityService, IdentityService>();
           
            services.AddSingleton<ICachedData, CachedData>();
            services.AddScoped<IJwtToken, JwtTokenCreator>();
            var jwtSettings = configuration
                                                .GetSection("JwtSettings")
                                                .Get<JwtSettings>()
                                                ?? throw new InvalidOperationException("JWT Settings are missing.");
            services
           .AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = jwtSettings.Issuer,

                   ValidateAudience = true,
                   ValidAudience = jwtSettings.Audience,

                   ValidateLifetime = true,

                   ValidateIssuerSigningKey = true,

                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(jwtSettings.Key)),

                   ClockSkew = TimeSpan.Zero
               };
           });


            return services; 
             
        }
    }
}
