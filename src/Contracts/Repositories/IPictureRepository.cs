using Domain.Picturies;

namespace Contracts.Repositories;

public interface IPictureRepository
{
    Task Add(Picture picture);
    Task<Picture?> GetUntracked(string bucket, string entityType, Guid entityId);
    Task<Picture?> Get(string bucket, string entityType, Guid entityId);
    void Remove(Picture picture);
}