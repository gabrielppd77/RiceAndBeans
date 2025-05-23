using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class MigrationExtensions
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