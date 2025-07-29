using Application.Common.ServiceHandler;
using Application.Picturies.GetPictureUrl;
using Contracts.Repositories;
using Domain.Common.Errors;
using Domain.Products;
using ErrorOr;

namespace Application.Products.GetProduct;

public class GetProductService(
    IProductRepository productRepository,
    IGetPictureUrlService getPictureUrlService) : IServiceHandler<GetProductRequest, ErrorOr<GetProductResponse>>
{
    public async Task<ErrorOr<GetProductResponse>> Handler(GetProductRequest request)
    {
        var product = await productRepository.GetByIdUntracked(request.ProductId);

        if (product is null) return Errors.Product.ProductNotFound;

        var urlImage = await getPictureUrlService.Handler(nameof(Product), product.Id);

        return new GetProductResponse(
            product.Id,
            product.Name,
            product.Description,
            urlImage,
            product.Price,
            product.CategoryId);
    }
}