using Microsoft.EntityFrameworkCore.Storage;
using AppCore.Infrastructure.Persistence.AppDbContext;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Infrastructure.Persistence.Repositories;
using AppCore.Infrastructure.Persistence.Entities;
using System.Reflection;
using System;

namespace AppCore.Infrastructure.Persistence.UnitOfWork
{
    public class BaseUnitOfWork: IBaseUnitOfWork, IDisposable
    {
        private readonly IBaseDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;

        public IBaseDbContext DataContext
        {
            get { return _dbContext; }
        }

        public BaseUnitOfWork(
            IBaseDbContext dbContext,
            IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }
        #region Transaction

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.SaveDataChangesAsync(cancellationToken);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _dbContext.SaveDataChangesAsync(cancellationToken);

            return true;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            await _dbContext.CommitTransactionAsync(transaction);
        }
        public void RollbackTransaction()
        {
            _dbContext.RollbackTransaction();
        }

        public bool HasActiveTransaction()
        {
            return _dbContext.HasActiveTransaction();
        }

        public IExecutionStrategy GetStrategy()
        {
            return _dbContext.GetStrategy();
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            if (_serviceProvider != null)
            {
                var repository = _serviceProvider.GetService<TRepository>();

                return repository;
            }

            return default;
        }

        public void Dispose()
        {
            try
            {
                GC.SuppressFinalize(this);
            }
            catch 
            {
                // Do notthing
            }
        }
        #endregion
    }
}
