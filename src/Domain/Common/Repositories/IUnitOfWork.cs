namespace Domain.Common.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();

    Task MigrateAsync();
}