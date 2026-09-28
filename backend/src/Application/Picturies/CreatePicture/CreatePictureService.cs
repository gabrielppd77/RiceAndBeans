using Contracts.Repositories;
using Contracts.Services.FileManager;
using Contracts.Works;
using Domain.Picturies;
using ErrorOr;

namespace Application.Picturies.CreatePicture;

public class CreatePictureService(
    IUploadFileService uploadFileService,
    IFileManagerSettings fileManagerSettings,
    IPictureRepository pictureRepository,
    IUnitOfWork unitOfWork) : ICreatePictureService
{
    public async Task<ErrorOr<string>> Handler(CreatePictureRequest request)
    {
        var bucket = fileManagerSettings.MainBucket;

        var result =
            await uploadFileService.UploadFileAsync(request.File.OpenReadStream(), bucket, request.Path);

        if (result.IsError) return result.Errors;

        var picture = new Picture(bucket, request.Path, request.EntityType, request.EntityId);

        await pictureRepository.Add(picture);

        await unitOfWork.SaveChangesAsync();

        return picture.GetUrl(fileManagerSettings.BaseUrl);
    }
}