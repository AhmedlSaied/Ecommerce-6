
using Ecommerce.Api.Extension;
using Ecommerce.Application;
using Ecommerce.Application.Profiles;
using Ecommerce.infrastructure;
using Ecommerce.infrastructure.Identity.JwtTokenCreator;
using Microsoft.Extensions.FileProviders;


namespace Ecommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInFrastructureService(builder.Configuration);
            builder.Services.AddApplicationServiceRegistration();
            builder.Services.Configure<UrlSetting>(builder.Configuration.GetSection("UrlSetting"));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddSwaggerGen();

            var app = builder.Build();
          await  app.MigrationAndSeedAsync();
            // Configure the HTTP request pipeline.
             if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
                RequestPath = "/Files"
            }
            );
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
