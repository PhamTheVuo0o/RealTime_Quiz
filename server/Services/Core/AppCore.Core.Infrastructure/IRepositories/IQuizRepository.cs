using AppCore.Core.Domain.Entities;
using AppCore.Core.Infrastructure.Queries.Responses;
using AppCore.Infrastructure.Persistence.Repositories;

namespace AppCore.Core.Infrastructure.IRepositories
{
    public interface IQuizRepository : IBaseRepository<Quiz>
    {
        Task<GetListQuizDbResponse> GetListQuizDb();
    }
}
