using Domain.Picturies;

namespace Contracts.Repositories;

public interface IPictureRepository
{
    Task Add(Picture picture);
    Task<string?> GetPathByEntityUntracked(string entityType, Guid entityId);
    Task<Picture?> GetByEntityUntracked(string entityType, Guid entityId);
}