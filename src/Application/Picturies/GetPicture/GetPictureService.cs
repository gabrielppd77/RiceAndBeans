using Contracts.Repositories;
using Contracts.Services.FileManager;
using Domain.Picturies;

namespace Application.Picturies.GetPicture;

public class GetPictureService(
    IPictureRepository pictureRepository,
    IFileManagerSettings fileManagerSettings) : IGetPictureService
{
    public async Task<Picture?> Handler(GetPictureRequest request)
    {
        return await pictureRepository.GetUntracked(fileManagerSettings.MainBucket, request.EntityType, request.EntityId);
    }
}