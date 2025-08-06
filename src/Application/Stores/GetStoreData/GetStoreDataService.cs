using Application.Common.ServiceHandler;
using Application.Picturies.GetPictureUrl;
using Contracts.Repositories;
using Contracts.Services.FileManager;
using Domain.Common.Errors;
using Domain.Companies;
using Domain.Products;
using ErrorOr;

namespace Application.Stores.GetStoreData;

public class GetStoreDataService(
    ICompanyRepository companyRepository,
    IGetPictureUrlService getPictureUrlService,
    IProductRepository productRepository,
    IFileManagerSettings fileManagerSettings) : IServiceHandler<GetStoreDataRequest, ErrorOr<GetStoreDataResponse>>
{
    public async Task<ErrorOr<GetStoreDataResponse>> Handler(GetStoreDataRequest request)
    {
        var company = await companyRepository.GetByPathUntracked(request.CompanyPath);

        if (company is null) return Errors.Company.CompanyNotFound;

        var products = await productRepository.GetAllWithCategoryRequiredByCompanyIdUntracked(company.Id);

        var productsPictures = await getPictureUrlService.Handler(nameof(Product), products.Select(x => x.Id));
        var urlImage = await getPictureUrlService.Handler(nameof(Company), company.Id);

        var productsResponse = new List<GetStoreProductResponse>();

        foreach (var product in products)
        {
            productsResponse.Add(new GetStoreProductResponse(
                product.Id,
                product.Name,
                product.Description,
                productsPictures
                    .Where(x => x.EntityId == product.Id)
                    .Select(x => x.GetUrl(fileManagerSettings.BaseUrl))
                    .FirstOrDefault(),
                product.Price,
                product.Category!.Name));
        }

        return new GetStoreDataResponse(
            company.Id,
            company.Name,
            company.Description,
            urlImage,
            productsResponse);
    }
}