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

    public async Task<List<Product>> GetAllByCompanyIdUntracked(Guid companyId)
    {
        return await context.Products
            .AsNoTracking()
            .Where(x => x.CompanyId == companyId)
            .OrderBy(x => x.Category!.Position).ThenBy(x => x.Position)
            .Include(x => x.Category)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllWithCategoryRequiredByCompanyIdUntracked(Guid companyId)
    {
        return await context.Products
            .AsNoTracking()
            .Where(x => x.CompanyId == companyId)
            .Where(x => x.CategoryId != null)
            .OrderBy(x => x.Category!.Position).ThenBy(x => x.Position)
            .Include(x => x.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetById(Guid productId)
    {
        return await context.Products.FirstOrDefaultAsync(x => x.Id == productId);
    }

    public void Remove(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<Product?> GetByIdUntracked(Guid productId)
    {
        return await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
    }
}