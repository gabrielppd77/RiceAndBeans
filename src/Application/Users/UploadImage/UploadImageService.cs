using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Contracts.Services.FileManager;
using Contracts.Works;
using Domain.Picturies;
using Domain.Users;
using ErrorOr;

namespace Application.Users.UploadImage;

public class UploadImageService(
    IUserAuthenticated userAuthenticated,
    IFileManagerSettings fileManagerSettings,
    IPictureRepository pictureRepository,
    IRemoveFileService removeFileService,
    IUploadFileService uploadFileService,
    IUnitOfWork unitOfWork)
    : IServiceHandler<UploadImageRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handler(UploadImageRequest request)
    {
        var userId = userAuthenticated.GetUserId();

        var bucket = fileManagerSettings.MainBucket;

        var pathToRemove = await pictureRepository.GetPathByEntityUntracked(nameof(User), userId);

        if (pathToRemove is not null)
        {
            var resultRemove = await removeFileService.RemoveFileAsync(bucket, pathToRemove);
            if (resultRemove.IsError) return resultRemove.Errors;
        }

        var path = $"user/{Guid.NewGuid().ToString()}{Path.GetExtension(request.File.FileName)}";

        var result =
            await uploadFileService.UploadFileAsync(request.File.OpenReadStream(), bucket, path);

        if (result.IsError) return result.Errors;

        var picture = new Picture(bucket, path, nameof(User), userId);

        await pictureRepository.Add(picture);

        await unitOfWork.SaveChangesAsync();

        return picture.GetUrl(fileManagerSettings.BaseUrl);
    }
}