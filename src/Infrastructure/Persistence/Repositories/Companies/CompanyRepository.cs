using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Companies;

public class CompanyRepository : IUploadImageCompanyRepository, IFormDataCompanyRepository
{
    private readonly IApplicationDbContext _context;

    public CompanyRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetCompanyByUser(Guid userId)
    {
        return await _context.Companies.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}