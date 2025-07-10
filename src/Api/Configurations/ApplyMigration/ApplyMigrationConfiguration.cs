using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations.ApplyMigration;

public static class ApplyMigrationConfiguration
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using IServiceScope scope = app.Services.CreateScope();

            using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }

        return app;
    }
}