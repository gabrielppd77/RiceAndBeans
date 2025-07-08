using ErrorOr;

namespace Application.Categories.CreateCategory;

public interface ICreateCategoryService
{
    Task<ErrorOr<Success>> Handler(CreateCategoryRequest request);
}