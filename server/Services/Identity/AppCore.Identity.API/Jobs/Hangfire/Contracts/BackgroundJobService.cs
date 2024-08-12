using Hangfire;
using System.Linq.Expressions;

namespace AppCore.Identity.API.Jobs.Hangfire.Contracts
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public void Enqueue(Expression<Func<Task>> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }
    }
}
