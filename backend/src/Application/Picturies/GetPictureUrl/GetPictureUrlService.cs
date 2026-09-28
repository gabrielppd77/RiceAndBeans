using Contracts.Repositories;
using Contracts.Services.FileManager;
using Domain.Picturies;

namespace Application.Picturies.GetPictureUrl;

public class GetPictureUrlService(
    IPictureRepository pictureRepository,
    IFileManagerSettings fileManagerSettings) : IGetPictureUrlService
{
    public async Task<string?> Handler(string entityType, Guid entityId)
    {
        var picture = await pictureRepository.GetUntracked(
            fileManagerSettings.MainBucket,
            entityType,
            entityId);
        return picture?.GetUrl(fileManagerSettings.BaseUrl);
    }

    public async Task<List<Picture>> Handler(string entityType, IEnumerable<Guid> entitiesId)
    {
        return await pictureRepository.GetAllUntracked(
            fileManagerSettings.MainBucket,
            entityType,
            entitiesId);
    }
}