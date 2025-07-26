using ErrorOr;

namespace Application.Picturies.RemovePicture;

public interface IRemovePictureService
{
    Task<ErrorOr<Success>> Handler(RemovePictureRequest request);
}