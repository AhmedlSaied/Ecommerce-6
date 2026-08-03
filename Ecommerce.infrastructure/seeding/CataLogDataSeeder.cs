using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Ecommerce.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.seeding
{
    public class CataLogDataSeeder(StoreDbContext DbContext, ILogger<CataLogDataSeeder> Logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                // ✅ Apply any pending migrations first
                if (DbContext.Database.GetPendingMigrations().Any())
                {
                    await DbContext.Database.MigrateAsync(ct);
                    Logger.LogInformation("Migrations applied successfully.");
                }

                await SeedBrandsAsync(ct);
                await SeedTypesAsync(ct);
                await SeedProductsAsync(ct);
                await SeedDeliveryMethodAsync(ct);
                

                Logger.LogInformation("Data seeding completed successfully.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while seeding data.");
                throw;
            }
        }

        // ─── Brands ───────────────────────────────────────────────
        private async Task SeedBrandsAsync(CancellationToken ct)
        {
            if (await DbContext.Brands.AnyAsync(ct))
            {
                Logger.LogInformation("Brands already seeded. Skipping.");
                return;
            }

            var filePath = Path.Combine(AppContext.BaseDirectory, "seeding", "brands.json");
            var brands = await ReadJsonFileAsync<List<Brand>>(filePath, ct);

            if (brands is null || brands.Count == 0)
            {
                Logger.LogWarning("No brands found in brands.json.");
                return;
            }

            await DbContext.Brands.AddRangeAsync(brands, ct);
            await DbContext.SaveChangesAsync(ct);

            Logger.LogInformation("Seeded {Count} brands.", brands.Count);
        }
        private async Task SeedDeliveryMethodAsync(CancellationToken ct)
        {
            if (await DbContext.DeliveryMethods.AnyAsync(ct))
            {
                Logger.LogInformation("Delivery Method already seeded. Skipping.");
                return;
            }

            var filePath = Path.Combine(AppContext.BaseDirectory, "seeding", "delivery.json");
            var deliveyMethod = await ReadJsonFileAsync<List<DeliveryMethod>>(filePath, ct);

            if (deliveyMethod is null || deliveyMethod.Count == 0)
            {
                Logger.LogWarning("No brands found in brands.json.");
                return;
            }

            await DbContext.DeliveryMethods.AddRangeAsync(deliveyMethod, ct);
            await DbContext.SaveChangesAsync(ct);

            Logger.LogInformation("Seeded {Count} brands.", deliveyMethod.Count);
        }

        // ─── Types ────────────────────────────────────────────────
        private async Task SeedTypesAsync(CancellationToken ct)
        {
            if (await DbContext.Types.AnyAsync(ct))
            {
                Logger.LogInformation("Types already seeded. Skipping.");
                return;
            }

            var filePath = Path.Combine(AppContext.BaseDirectory, "seeding", "types.json");
            var types = await ReadJsonFileAsync<List<ProductType>>(filePath, ct);

            if (types is null || types.Count == 0)
            {
                Logger.LogWarning("No types found in types.json.");
                return;
            }

            await DbContext.Types.AddRangeAsync(types, ct);
            await DbContext.SaveChangesAsync(ct);

            Logger.LogInformation("Seeded {Count} types.", types.Count);
        }

        // ─── Products ─────────────────────────────────────────────
        private async Task SeedProductsAsync(CancellationToken ct)
        {
            if (await DbContext.Products.AnyAsync(ct))
            {
                Logger.LogInformation("Products already seeded. Skipping.");
                return;
            }

            var filePath = Path.Combine(AppContext.BaseDirectory, "seeding", "products.json");
            var products = await ReadJsonFileAsync<List<Product>>(filePath, ct);

            if (products is null || products.Count == 0)
            {
                Logger.LogWarning("No products found in products.json.");
                return;
            }

            await DbContext.Products.AddRangeAsync(products, ct);
            await DbContext.SaveChangesAsync(ct);

            Logger.LogInformation("Seeded {Count} products.", products.Count);
        }

        // ─── Helper ───────────────────────────────────────────────
        private static async Task<T?> ReadJsonFileAsync<T>(string filePath, CancellationToken ct)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Seed file not found: {filePath}");

            await using var stream = File.OpenRead(filePath);

            return await JsonSerializer.DeserializeAsync<T>(stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, ct);
        }
    }
}
