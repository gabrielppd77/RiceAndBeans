using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Domain.Companies;
using Domain.Users;

namespace Application.Common.Interfaces.Database;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Company> Companies { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
