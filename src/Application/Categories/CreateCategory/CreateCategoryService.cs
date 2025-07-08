using Application.Common.Interfaces.Authentication;
using Domain.Categories;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.Categories.CreateCategory;

public class CreateCategoryService(
    IUserAuthenticated userAuthenticated,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICreateCategoryService
{
    public async Task<ErrorOr<Success>> Handler(CreateCategoryRequest request)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var category = new Category(companyId, request.Name);

        await categoryRepository.Add(category);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}