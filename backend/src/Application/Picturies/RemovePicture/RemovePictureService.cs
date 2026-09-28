using Contracts.Repositories;
using Contracts.Services.FileManager;
using Contracts.Works;
using ErrorOr;

namespace Application.Picturies.RemovePicture;

public class RemovePictureService(
    IPictureRepository pictureRepository,
    IFileManagerSettings fileManagerSettings,
    IRemoveFileService removeFileService,
    IUnitOfWork unitOfWork) : IRemovePictureService
{
    public async Task<ErrorOr<Success>> Handler(string entityType, Guid entityId)
    {
        var picture = await pictureRepository.Get(fileManagerSettings.MainBucket, entityType, entityId);

        if (picture is null) return Result.Success;

        var result = await removeFileService.RemoveFileAsync(picture.Bucket, picture.Path);

        if (result.IsError) return result.Errors;

        pictureRepository.Remove(picture);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}