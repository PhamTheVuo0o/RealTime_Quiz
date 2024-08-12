using System.Linq.Expressions;

namespace AppCore.Identity.API.Jobs.Hangfire.Contracts
{
    public interface IBackgroundJobService
    {
        void Enqueue(Expression<Func<Task>> methodCall);
    }
}
