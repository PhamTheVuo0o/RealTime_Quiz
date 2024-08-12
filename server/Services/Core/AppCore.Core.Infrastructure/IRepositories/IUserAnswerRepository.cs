using AppCore.Core.Domain.Entities;
using AppCore.Core.Infrastructure.Queries.Requests;
using AppCore.Core.Infrastructure.Queries.Responses;
using AppCore.Infrastructure.Persistence.Repositories;

namespace AppCore.Core.Infrastructure.IRepositories
{
    public interface IUserAnswerRepository : IBaseRepository<UserAnswer>
    {
        Task<SubmitAnswerDbResponse> SubmitAnswerDb(SubmitAnswerDbRequest request);
        Task<GetListUserAnswerByQuizIdDbResponse> GetListUserAnswerByQuizIdDb(GetListUserAnswerByQuizIdDbRequest request);
    }
}
