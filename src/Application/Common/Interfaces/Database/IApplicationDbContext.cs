using Domain.Categories;
using Domain.Companies;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Database;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Company> Companies { get; set; }

    DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task MigrateAsync();
}