using Application.Common.ServiceHandler;
using Application.Picturies.RemovePicture;
using Contracts.Repositories;
using Contracts.Works;
using Domain.Common.Errors;
using Domain.Products;
using ErrorOr;

namespace Application.Products.RemoveProduct;

public class RemoveProductService(
    IProductRepository productRepository,
    IRemovePictureService removePictureService,
    IUnitOfWork unitOfWork) : IServiceHandler<RemoveProductRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(RemoveProductRequest request)
    {
        var product = await productRepository.GetById(request.ProductId);

        if (product is null) return Errors.Product.ProductNotFound;

        var result = await removePictureService.Handler(
            new RemovePictureRequest(
                nameof(Product),
                product.Id));

        if (result.IsError) return result.Errors;

        productRepository.Remove(product);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}