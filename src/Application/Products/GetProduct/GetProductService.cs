using Application.Common.ServiceHandler;
using Application.Picturies.GetPicture;
using Contracts.Repositories;
using Contracts.Services.FileManager;
using Domain.Common.Errors;
using Domain.Products;
using ErrorOr;

namespace Application.Products.GetProduct;

public class GetProductService(
    IProductRepository productRepository,
    IGetPictureService getPictureService,
    IFileManagerSettings fileManagerSettings) : IServiceHandler<GetProductRequest, ErrorOr<GetProductResponse>>
{
    public async Task<ErrorOr<GetProductResponse>> Handler(GetProductRequest request)
    {
        var product = await productRepository.GetByIdUntracked(request.ProductId);

        if (product is null) return Errors.Product.ProductNotFound;

        var picture = await getPictureService.Handler(
            new GetPictureRequest(
                nameof(Product),
                product.Id));

        return new GetProductResponse(
            product.Id,
            product.Name,
            product.Description,
            picture?.GetUrl(fileManagerSettings.BaseUrl),
            product.Price,
            product.CategoryId);
    }
}