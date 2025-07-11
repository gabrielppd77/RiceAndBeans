using Application.Common.ServiceHandler;
using Contracts.Services.Authentication;
using Domain.Categories;
using Contracts.Repositories;
using Contracts.Works;
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

        var lastPosition = await categoryRepository.GetLastPositionByCompanyIdUntracked(companyId);

        var currentPosition = lastPosition + 1;

        var category = new Category(companyId, request.Name, currentPosition);

        await categoryRepository.Add(category);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}