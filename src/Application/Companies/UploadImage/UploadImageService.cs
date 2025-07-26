using Application.Common.ServiceHandler;
using Application.Picturies.CreatePicture;
using Application.Picturies.RemovePicture;
using Contracts.Services.Authentication;
using Domain.Companies;
using ErrorOr;

namespace Application.Companies.UploadImage;

public class UploadImageService(
    IUserAuthenticated userAuthenticated,
    IRemovePictureService removePictureService,
    ICreatePictureService createPictureService)
    : IServiceHandler<UploadImageRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handler(UploadImageRequest request)
    {
        var entityType = nameof(Company);
        var entityId = userAuthenticated.GetCompanyId();
        var file = request.File;

        var resultRemove = await removePictureService.Handler(
            new RemovePictureRequest(
                entityType,
                entityId));

        if (resultRemove.IsError) return resultRemove.Errors;

        var path = $"company/{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

        return await createPictureService.Handler(
            new CreatePictureRequest(
                file,
                path,
                entityType,
                entityId));
    }
}