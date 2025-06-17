using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.FileService;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Companies.UploadImage;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ErrorOr<string>>
{
    private readonly IUploadFileService _uploadFileService;
    private readonly IUserAuthenticated _userAuthenticated;
    private readonly IUploadImageCompanyRepository _uploadImageCompanyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadImageCommandHandler(
        IUploadFileService uploadFileService,
        IUserAuthenticated userAuthenticated,
        IUploadImageCompanyRepository uploadImageCompanyRepository,
        IUnitOfWork unitOfWork)
    {
        _uploadFileService = uploadFileService;
        _userAuthenticated = userAuthenticated;
        _uploadImageCompanyRepository = uploadImageCompanyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var userId = _userAuthenticated.GetUserId();

        var company = await _uploadImageCompanyRepository.GetCompanyByUser(userId);

        if (company is null) return Errors.Company.CompanyNotFound;

        //todo: pass this to domain, "company path"
        var fileName = $"company/{company.Id.ToString()}{Path.GetExtension(request.File.FileName)}";

        var urlImageUploaded =
            await _uploadFileService.UploadFileAsync(request.File.OpenReadStream(), fileName);

        if (urlImageUploaded is null) return Errors.UploadFile.UnexpectedError;

        company.UpdateImage(urlImageUploaded);

        await _unitOfWork.SaveChangesAsync();

        return urlImageUploaded;
    }
}