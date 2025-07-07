using Application.Common.Interfaces.Database;
using Domain.Common.Repositories;
using Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Companies;

public class CompanyRepository(IApplicationDbContext context) : ICompanyRepository
{
    public async Task<Company?> GetById(Guid companyId)
    {
        return await context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
    }

    public async Task<Company?> GetByIdUntracked(Guid companyId)
    {
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == companyId);
    }
}