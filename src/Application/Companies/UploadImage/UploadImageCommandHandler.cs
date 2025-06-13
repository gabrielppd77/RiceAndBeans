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
    private readonly IUserAuthenticated _serAuthenticated;
    private readonly IUploadImageCompanyRepository _uploadImageCompanyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadImageCommandHandler(
        IUploadFileService uploadFileService,
        IUserAuthenticated serAuthenticated,
        IUploadImageCompanyRepository uploadImageCompanyRepository,
        IUnitOfWork unitOfWork)
    {
        _uploadFileService = uploadFileService;
        _serAuthenticated = serAuthenticated;
        _uploadImageCompanyRepository = uploadImageCompanyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var userId = _serAuthenticated.GetUserId();

        var company = await _uploadImageCompanyRepository.GetCompanyByUser(userId);

        if (company is null) return Errors.Company.CompanyNotFound;

        var fileName = $"company/{company.Id.ToString()}{Path.GetExtension(request.File.FileName)}";

        var urlImageUploaded =
            await _uploadFileService.UploadFileAsync(request.File.OpenReadStream(), fileName);

        if (urlImageUploaded is null) return Errors.UploadFile.UnexpectedError;

        company.UrlImage = urlImageUploaded;

        await _unitOfWork.SaveChangesAsync();

        return urlImageUploaded;
    }
}