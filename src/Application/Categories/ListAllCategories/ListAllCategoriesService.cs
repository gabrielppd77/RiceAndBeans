using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Services.Authentication;

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

        return categories.Select(x => new CategoryResponse(x.Id, x.Name, x.Position));
    }
}