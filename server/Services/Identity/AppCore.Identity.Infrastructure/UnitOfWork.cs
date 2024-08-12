using AppCore.Identity.Infrastructure.IRepositories.Account;
using AppCore.Infrastructure.Persistence.AppDbContext;
using AppCore.Infrastructure.Persistence.UnitOfWork;

namespace AppCore.Identity.Infrastructure
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(IBaseDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
        }
        public IAppUserRepository appUserRepository => GetRepository<IAppUserRepository>();
    }
}