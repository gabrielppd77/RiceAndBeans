using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.FileManager;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Companies.UploadImage;

public class UploadImageCommandHandler(
    IUploadFileService uploadFileService,
    IRemoveFileService removeFileService,
    IUserAuthenticated userAuthenticated,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadImageCommand, ErrorOr<string>>
{
    private const string FolderPathImage = "company";

    public async Task<ErrorOr<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
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