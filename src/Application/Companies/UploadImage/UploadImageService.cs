using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Contracts.Services.FileManager;
using Contracts.Works;
using Domain.Companies;
using Domain.Picturies;
using ErrorOr;

namespace Application.Companies.UploadImage;

public class UploadImageService(
    IUserAuthenticated userAuthenticated,
    IPictureRepository pictureRepository,
    IRemoveFileService removeFileService,
    IUploadFileService uploadFileService,
    IFileManagerSettings fileManagerSettings,
    IUnitOfWork unitOfWork)
    : IServiceHandler<UploadImageRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handler(UploadImageRequest request)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var bucket = fileManagerSettings.MainBucket;

        var pathToRemove = await pictureRepository.GetPathByEntityUntracked(nameof(Company), companyId);

        if (pathToRemove is not null)
        {
            var resultRemove = await removeFileService.RemoveFileAsync(bucket, pathToRemove);
            if (resultRemove.IsError) return resultRemove.Errors;
        }

        var path = $"company/{Guid.NewGuid().ToString()}{Path.GetExtension(request.File.FileName)}";

        var result =
            await uploadFileService.UploadFileAsync(request.File.OpenReadStream(), bucket, path);

        if (result.IsError) return result.Errors;

        var picture = new Picture(bucket, path, nameof(Company), companyId);

        await pictureRepository.Add(picture);

        await unitOfWork.SaveChangesAsync();

        return picture.GetUrl(fileManagerSettings.BaseUrl);
    }
}