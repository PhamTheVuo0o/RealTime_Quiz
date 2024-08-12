using AppCore.Infrastructure.Persistence.UnitOfWork;
using AppCore.Identity.Infrastructure.IRepositories.Account;

namespace AppCore.Identity.Infrastructure
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IAppUserRepository appUserRepository { get; }
    }
}
