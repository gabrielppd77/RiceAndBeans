using Api.Controllers.Categories.Contracts;
using Application.Categories.CreateCategory;
using Mapster;

namespace Api.Common.Mapping;

public class CategoriesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>();
    }
}