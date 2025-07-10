using Application.Common.ServiceHandler;
using Contracts.Services.Authentication;
using Contracts.Repositories;

namespace Application.Categories.ListAllCategories;

public class ListAllCategoriesService(
    IUserAuthenticated userAuthenticated,
    ICategoryRepository categoryRepository)
    : IServiceHandler<Unit, IEnumerable<CategoryResponse>>
{
    public async Task<IEnumerable<CategoryResponse>> Handler(Unit _)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var categories = await categoryRepository.GetAllByCompanyIdUntracked(companyId);

        return categories.Select(x => new CategoryResponse(x.Id, x.Name));
    }
}