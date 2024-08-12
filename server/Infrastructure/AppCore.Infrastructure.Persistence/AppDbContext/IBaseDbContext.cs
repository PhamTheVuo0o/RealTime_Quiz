﻿using Microsoft.EntityFrameworkCore.Storage;

namespace AppCore.Infrastructure.Persistence.AppDbContext
{
    public interface IBaseDbContext : IDisposable
    {
        Task<int> SaveDataChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollbackTransaction();
        bool HasActiveTransaction();
        IExecutionStrategy GetStrategy();
    }
}
