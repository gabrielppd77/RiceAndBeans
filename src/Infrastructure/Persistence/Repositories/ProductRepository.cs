using Contracts.Repositories;
using Domain.Products;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<int> GetLastPositionByCompanyIdUntracked(Guid companyId)
    {
        return await context.Products
            .AsNoTracking()
            .Where(x => x.CompanyId == companyId)
            .MaxAsync(c => (int?)c.Position) ?? 0;
    }

    public async Task Add(Product product)
    {
        await context.Products.AddAsync(product);
    }
}