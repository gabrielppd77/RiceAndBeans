namespace Application.Categories.ListAllCategories;

public interface IListAllCategoriesService
{
    Task<IEnumerable<CategoryResponse>> Handle();
}