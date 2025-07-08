using ErrorOr;

namespace Application.Companies.UploadImage;

public interface IUploadImageService
{
    Task<ErrorOr<string>> Handle(UploadImageRequest request);
}