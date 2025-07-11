using Contracts.Repositories;
using Domain.Categories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public async Task Add(Category category)
    {
        await context.Categories.AddAsync(category);
    }

    public async Task<IEnumerable<Category>> GetAllByCompanyIdUntracked(Guid companyId)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(x => x.CompanyId == companyId)
            .OrderBy(x => x.Position)
            .ToListAsync();
    }

    public async Task<Category?> GetById(Guid categoryId)
    {
        return await context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
    }

    public void Remove(Category category)
    {
        context.Categories.Remove(category);
    }

    public async Task<int> GetLastPositionByCompanyIdUntracked(Guid companyId)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(x => x.CompanyId == companyId)
            .MaxAsync(c => (int?)c.Position) ?? 0;
    }

    public async Task<IEnumerable<Category>> GetAllByIds(IEnumerable<Guid> categoriesId)
    {
        return await context.Categories
            .Where(x => categoriesId.Contains(x.Id))
            .ToListAsync();
    }
}