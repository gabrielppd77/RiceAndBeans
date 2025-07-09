namespace Contracts.Works;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}