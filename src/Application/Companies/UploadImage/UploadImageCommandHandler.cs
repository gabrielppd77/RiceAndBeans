using Application.Common.Interfaces.FileService;
using ErrorOr;
using MediatR;

namespace Application.Companies.UploadImage;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ErrorOr<Unit>>
{
    private readonly IUploadFileService _uploadFileService;

    public UploadImageCommandHandler(IUploadFileService uploadFileService)
    {
        _uploadFileService = uploadFileService;
    }

    public async Task<ErrorOr<Unit>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        await _uploadFileService.UploadFileAsync(request.File.OpenReadStream(), "teste-buckets", "imagem.jpg", "image/jpeg");

        return Unit.Value;
    }
}
