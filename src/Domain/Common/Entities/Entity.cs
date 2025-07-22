namespace Domain.Common.Entities;

public abstract class Entity : IEntity
{
    public Guid Id { get; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}