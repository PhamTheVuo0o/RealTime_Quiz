using AppCore.Core.Domain.Entities;
using AppCore.Core.Infrastructure.Queries.Requests;
using AppCore.Core.Infrastructure.Queries.Responses;
using AppCore.Infrastructure.Persistence.Repositories;

namespace AppCore.Core.Infrastructure.IRepositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<GetListQuestionByQuizIdDbResponse> GetListQuestionByQuizIdDb(GetListQuestionByQuizIdDbRequest request);
    }
}
