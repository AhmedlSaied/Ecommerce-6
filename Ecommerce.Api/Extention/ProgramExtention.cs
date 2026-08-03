//using Ecommerce.infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Ecommerce.Api.Extention
//{
//    public static class ProgramExtention
//    {
//        public static async Task MigrationAndSeedAsync(this WebApplication app)
//        {
//            var scope = app.Services.CreateScope();
//            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
//            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//            var pendingMigrations =await dbContext.Database.GetAppliedMigrationsAsync();

//            if (pendingMigrations.Count()>0)
//            {
//                logger.LogInformation("Applying migrations...");
//                await dbContext.Database.MigrateAsync();
//                logger.LogInformation("Migrations applied successfully.");
//            }
//            else
//            {
//                logger.LogInformation("No pending migrations found.");
//            }
//        }
//    }
//}
using Ecommerce.Domain.Contracts;
using Ecommerce.infrastructure.Data;
using Ecommerce.infrastructure.Identity.Data;
using Ecommerce.infrastructure.seeding;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Extension
{
    public static class ProgramExtension
    {
        public static async Task MigrationAndSeedAsync(
            this WebApplication app,
            CancellationToken cancellationToken = default)
        {
            await using var scope =
                app.Services.CreateAsyncScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<StoreDbContext>();

            var catalogDataSeeder =
      scope.ServiceProvider
          .GetRequiredKeyedService<IDataSeeder>("CataLog");

            var identityDataSeeder =
                scope.ServiceProvider
                    .GetRequiredKeyedService<IDataSeeder>("Identity");

            var logger =
                scope.ServiceProvider
                    .GetRequiredService<ILogger<Program>>();

            try
            {
                var pendingMigrations =
                    (await dbContext.Database
                        .GetPendingMigrationsAsync(cancellationToken))
                    .ToList();

                if (pendingMigrations.Any())
                {
                    logger.LogInformation(
                        "Applying {Count} pending migrations...",
                        pendingMigrations.Count);

                    await dbContext.Database
                        .MigrateAsync(cancellationToken);

                    logger.LogInformation(
                        "Migrations applied successfully.");
                }
                else
                {
                    logger.LogInformation(
                        "No pending migrations found.");
                }

                logger.LogInformation(
                    "Starting database seeding...");

                await catalogDataSeeder.SeedDataAsync(
                    cancellationToken);
                await identityDataSeeder.SeedDataAsync(
                   cancellationToken);

                logger.LogInformation(
                    "Database seeding completed successfully.");
            }
            catch (Exception exception)
            {
                logger.LogCritical(
                    exception,
                    "An error occurred while migrating or seeding the database.");

                throw;
            }
        }
    }
}