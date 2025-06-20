using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Companies;

public class CompanyRepository(IApplicationDbContext context) : ICompanyRepository
{
    public async Task<Company?> GetByUserId(Guid userId)
    {
        return await context.Companies.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<Company?> GetByUserIdUntracked(Guid userId)
    {
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
    }
}