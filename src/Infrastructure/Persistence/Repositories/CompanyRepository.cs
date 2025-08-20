using Contracts.Repositories;
using Domain.Companies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CompanyRepository(ApplicationDbContext context) : ICompanyRepository
{
    public async Task<Company?> GetById(Guid companyId)
    {
        return await context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
    }

    public async Task<Company?> GetByIdUntracked(Guid companyId)
    {
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == companyId);
    }

    public async Task<Company?> GetByPathUntracked(string companyPath)
    {
        return await context.Companies
            .AsNoTracking()
            .Where(x => x.Path == companyPath)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Company>> GetAllUntracked()
    {
        return await context.Companies.AsNoTracking().ToListAsync();
    }
}