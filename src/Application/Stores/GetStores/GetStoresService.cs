using Application.Common.ServiceHandler;
using Application.Picturies.GetPictureUrl;
using Contracts.Repositories;
using Contracts.Services.FileManager;
using Domain.Companies;
using ErrorOr;

namespace Application.Stores.GetStores;

public class GetStoresService(
    ICompanyRepository companyRepository,
    IGetPictureUrlService getPictureUrlService,
    IFileManagerSettings fileManagerSettings) : IServiceHandler<Unit, ErrorOr<List<StoreData>>>
{
    public async Task<ErrorOr<List<StoreData>>> Handler(Unit request)
    {
        var companies = await companyRepository.GetAllUntracked();

        var companiesPictures = await getPictureUrlService.Handler(nameof(Company), companies.Select(x => x.Id));

        var data = new List<StoreData>();

        foreach (var company in companies)
        {
            var companyPicture = companiesPictures
                .Where(x => x.EntityId == company.Id)
                .Select(x => x.GetUrl(fileManagerSettings.BaseUrl))
                .FirstOrDefault();

            data.Add(new StoreData(company.Id, company.Name, company.Path, companyPicture));
        }

        return data;
    }
}