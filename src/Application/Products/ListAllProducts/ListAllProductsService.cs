using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Services.Authentication;

namespace Application.Products.ListAllProducts;

public class ListAllProductsService(
    IUserAuthenticated userAuthenticated,
    IProductRepository productRepository) : IServiceHandler<Unit, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handler(Unit request)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var products = await productRepository.GetAllByCompanyIdUntracked(companyId);

        return products.Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Price, x.Category?.Name ?? ""));
    }
}