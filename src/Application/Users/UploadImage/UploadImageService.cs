using Application.Common.ServiceHandler;
using Contracts.Services.Authentication;
using Contracts.Services.FileManager;
using Domain.Common.Errors;
using Contracts.Repositories;
using Contracts.Works;
using ErrorOr;

namespace Application.Users.UploadImage;

public class UploadImageService(
    IUploadFileService uploadFileService,
    IRemoveFileService removeFileService,
    IUserAuthenticated userAuthenticated,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IServiceHandler<UploadImageRequest, ErrorOr<string>>
{
    private const string FolderPathImage = "user";

    public async Task<ErrorOr<string>> Handler(UploadImageRequest request)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await userRepository.GetById(userId);

        if (user is null) return Errors.User.UserNotFound;

        if (user.UrlImage is not null)
        {
            var uri = new Uri(user.UrlImage);
            var fileNameToRemove = $"{FolderPathImage}/{Path.GetFileName(uri.LocalPath)}";
            var resultRemove = await removeFileService.RemoveFileAsync(fileNameToRemove);

            if (resultRemove.IsError)
            {
                return resultRemove.Errors;
            }
        }

        var fileName = $"{FolderPathImage}/{user.Id.ToString()}{Path.GetExtension(request.File.FileName)}";

        var resultUpload =
            await uploadFileService.UploadFileAsync(request.File.OpenReadStream(), fileName);

        if (resultUpload.IsError)
        {
            return resultUpload.Errors;
        }

        var urlImageUploaded = resultUpload.Value;

        user.UpdateImage(urlImageUploaded);

        await unitOfWork.SaveChangesAsync();

        return urlImageUploaded;
    }
}