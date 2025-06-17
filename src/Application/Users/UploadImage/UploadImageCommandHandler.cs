using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.FileManager;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Users.UploadImage;

public class UploadImageCommandHandler(
    IUploadFileService uploadFileService,
    IRemoveFileService removeFileService,
    IUserAuthenticated userAuthenticated,
    IUploadImageUserRepository uploadImageUserRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadImageCommand, ErrorOr<string>>
{
    private readonly string _folderPathImage = "user";

    public async Task<ErrorOr<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await uploadImageUserRepository.GetUserById(userId);

        if (user is null) return Errors.User.UserNotFound;

        if (user.UrlImage is not null)
        {
            var uri = new Uri(user.UrlImage);
            var fileNameToRemove = $"{_folderPathImage}/{Path.GetFileName(uri.LocalPath)}";
            var resultRemove = await removeFileService.RemoveFileAsync(fileNameToRemove);

            if (resultRemove.IsError)
            {
                return resultRemove.Errors;
            }
        }

        var fileName = $"{_folderPathImage}/{user.Id.ToString()}{Path.GetExtension(request.File.FileName)}";

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