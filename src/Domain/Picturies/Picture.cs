using Domain.Common.Entities;

namespace Domain.Picturies;

public class Picture : Entity
{
    public string Bucket { get; private set; }
    public string Path { get; private set; }
    public string EntityType { get; private set; }
    public Guid EntityId { get; private set; }

    protected Picture()
    {
    }

    public Picture(string bucket, string path, string entityType, Guid entityId)
    {
        Bucket = bucket;
        Path = path;
        EntityType = entityType;
        EntityId = entityId;
    }

    public string GetUrl(string baseUrl) => $"{baseUrl}/{Bucket}/{Path}";
}