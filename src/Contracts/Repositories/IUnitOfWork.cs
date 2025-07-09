namespace Contracts.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();

    Task MigrateAsync();
}