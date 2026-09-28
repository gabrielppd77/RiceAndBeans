using ErrorOr;

namespace Application.Picturies.CreatePicture;

public interface ICreatePictureService
{
    Task<ErrorOr<string>> Handler(CreatePictureRequest request);
}