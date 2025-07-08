using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.FileManager;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.Companies.UploadImage;

public class UploadImageService(
    IUploadFileService uploadFileService,
    IRemoveFileService removeFileService,
    IUserAuthenticated userAuthenticated,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : IUploadImageService
{
    private const string FolderPathImage = "company";

    public async Task<ErrorOr<string>> Handle(UploadImageRequest request)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var company = await companyRepository.GetById(companyId);

        if (company is null) return Errors.Company.CompanyNotFound;

        if (company.UrlImage is not null)
        {
            var uri = new Uri(company.UrlImage);
            var fileNameToRemove = $"{FolderPathImage}/{Path.GetFileName(uri.LocalPath)}";
            var resultRemove = await removeFileService.RemoveFileAsync(fileNameToRemove);

            if (resultRemove.IsError)
            {
                return resultRemove.Errors;
            }
        }

        var fileName = $"{FolderPathImage}/{company.Id.ToString()}{Path.GetExtension(request.File.FileName)}";

        var resultUpload =
            await uploadFileService.UploadFileAsync(request.File.OpenReadStream(), fileName);

        if (resultUpload.IsError)
        {
            return resultUpload.Errors;
        }

        var urlImageUploaded = resultUpload.Value;

        company.UpdateImage(urlImageUploaded);

        await unitOfWork.SaveChangesAsync();

        return urlImageUploaded;
    }
}