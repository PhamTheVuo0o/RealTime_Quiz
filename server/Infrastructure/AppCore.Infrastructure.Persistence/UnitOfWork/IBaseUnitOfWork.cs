using Microsoft.EntityFrameworkCore.Storage;
using AppCore.Infrastructure.Persistence.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Persistence.UnitOfWork
{
    public interface IBaseUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollbackTransaction();
        bool HasActiveTransaction();
        IExecutionStrategy GetStrategy();
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
