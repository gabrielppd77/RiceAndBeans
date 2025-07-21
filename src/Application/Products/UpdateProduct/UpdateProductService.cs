using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Works;
using Domain.Common.Errors;
using ErrorOr;

namespace Application.Products.UpdateProduct;

public class UpdateProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IServiceHandler<UpdateProductRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(UpdateProductRequest request)
    {
        var product = await productRepository.GetById(request.ProductId);

        if (product is null) return Errors.Product.ProductNotFound;

        product.UpdateFormFields(request.CategoryId, request.Name, request.Description, request.Price);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}