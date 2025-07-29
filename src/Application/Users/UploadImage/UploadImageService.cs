using Application.Common.ServiceHandler;
using Application.Picturies.CreatePicture;
using Application.Picturies.RemovePicture;
using Contracts.Services.Authentication;
using Domain.Users;
using ErrorOr;

namespace Application.Users.UploadImage;

public class UploadImageService(
    IUserAuthenticated userAuthenticated,
    IRemovePictureService removePictureService,
    ICreatePictureService createPictureService)
    : IServiceHandler<UploadImageRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handler(UploadImageRequest request)
    {
        var entityType = nameof(User);
        var entityId = userAuthenticated.GetUserId();
        var file = request.File;

        var resultRemove = await removePictureService.Handler(entityType, entityId);

        if (resultRemove.IsError) return resultRemove.Errors;

        var path = $"user/{Guid.NewGuid().ToString()}{Path.GetExtension(request.File.FileName)}";

        return await createPictureService.Handler(
            new CreatePictureRequest(
                file,
                path,
                entityType,
                entityId));
    }
}