using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Works;
using Domain.Common.Errors;
using ErrorOr;

namespace Application.Products.RemoveProduct;

public class RemoveProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IServiceHandler<RemoveProductRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(RemoveProductRequest request)
    {
        var product = await productRepository.GetById(request.ProductId);

        if (product is null) return Errors.Product.ProductNotFound;

        productRepository.Remove(product);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}