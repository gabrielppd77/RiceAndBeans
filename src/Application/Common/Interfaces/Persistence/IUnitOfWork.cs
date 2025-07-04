﻿namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();

    Task MigrateAsync();
}