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
}