using Domain.Picturies;

namespace Application.Picturies.GetPicture;

public interface IGetPictureService
{
    Task<Picture?> Handler(GetPictureRequest request);
}