using Domain.Picturies;

namespace Application.Picturies.GetPictureUrl;

public interface IGetPictureUrlService
{
    Task<string?> Handler(string entityType, Guid entityId);
    Task<List<Picture>> Handler(string entityType, IEnumerable<Guid> entitiesId);
}