using Application.Common.ServiceHandler;
using Application.Picturies.CreatePicture;
using Application.Picturies.RemovePicture;
using Domain.Products;
using ErrorOr;

namespace Application.Products.UploadImage;

public class UploadImageService(
    IRemovePictureService removePictureService,
    ICreatePictureService createPictureService) : IServiceHandler<UploadImageRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handler(UploadImageRequest request)
    {
        var entityType = nameof(Product);
        var entityId = request.ProductId;
        var file = request.File;

        var resultRemove = await removePictureService.Handler(entityType, entityId);

        if (resultRemove.IsError) return resultRemove.Errors;

        var path = $"product/{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

        return await createPictureService.Handler(
            new CreatePictureRequest(
                file,
                path,
                entityType,
                entityId));
    }
}