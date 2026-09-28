using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Contracts.Works;
using Domain.Products;
using ErrorOr;

namespace Application.Products.CreateProduct;

public class CreateProductService(
    IUserAuthenticated userAuthenticated,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IServiceHandler<CreateProductRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(CreateProductRequest request)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var lastPosition = await productRepository.GetLastPositionByCompanyIdUntracked(companyId);

        var currentPosition = lastPosition + 1;

        var product = new Product(
            companyId,
            request.CategoryId,
            request.Name,
            request.Description,
            currentPosition,
            request.Price);

        await productRepository.Add(product);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}