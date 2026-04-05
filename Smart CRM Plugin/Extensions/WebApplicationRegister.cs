using CRM.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Smart_CRM_Plugin.Extensions
{
    public static class WebApplicationRegister
    {
        public static async Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CRMdbContext>();

            var penedingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (penedingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}
