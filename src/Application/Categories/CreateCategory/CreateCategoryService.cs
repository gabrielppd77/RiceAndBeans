using Contracts.Services.Authentication;
using Application.Common.Services;
using Domain.Categories;
using Contracts.Repositories;
using ErrorOr;

namespace Application.Categories.CreateCategory;

public class CreateCategoryService(
    IUserAuthenticated userAuthenticated,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IServiceHandler<CreateCategoryRequest, ErrorOr<Success>>
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