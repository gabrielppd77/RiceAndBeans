using Application.Common.Interfaces.Authentication;
using Domain.Common.Repositories;

namespace Application.Categories.ListAllCategories;

public class ListAllCategoriesService(IUserAuthenticated userAuthenticated, ICategoryRepository categoryRepository)
    : IListAllCategoriesService
{
    public async Task<IEnumerable<CategoryResponse>> Handle()
    {
        var companyId = userAuthenticated.GetCompanyId();

        var categories = await categoryRepository.GetAllByCompanyIdUntracked(companyId);

        return categories.Select(x => new CategoryResponse(x.Id, x.Name));
    }
}