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
    IUploadImageCompanyRepository uploadImageCompanyRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadImageCommand, ErrorOr<string>>
{
    private readonly string _folderPathImage = "company";

    public async Task<ErrorOr<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var company = await uploadImageCompanyRepository.GetCompanyByUser(userId);

        if (company is null) return Errors.Company.CompanyNotFound;

        if (company.UrlImage is not null)
        {
            var uri = new Uri(company.UrlImage);
            var fileNameToRemove = $"{_folderPathImage}/{Path.GetFileName(uri.LocalPath)}";
            var resultRemove = await removeFileService.RemoveFileAsync(fileNameToRemove);

            if (resultRemove.IsError)
            {
                return resultRemove.Errors;
            }
        }

        var fileName = $"{_folderPathImage}/{company.Id.ToString()}{Path.GetExtension(request.File.FileName)}";

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