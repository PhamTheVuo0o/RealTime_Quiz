using AppCore.Core.Domain.Entities;
using AppCore.Core.Domain.Models;
using AppCore.Core.Infrastructure.IRepositories;
using AppCore.Core.Infrastructure.Queries.Requests;
using AppCore.Core.Infrastructure.Queries.Responses;
using AppCore.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Core.Infrastructure.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        DataContext _context;
        IHttpContextAccessor _httpContextAccessor;
        public QuestionRepository(DataContext context,
            IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GetListQuestionByQuizIdDbResponse> GetListQuestionByQuizIdDb(GetListQuestionByQuizIdDbRequest request)
        {
            var query = _context.Questions.Select(x => new QuestionModel()
            {
                Id = x.Id,
                QuizId = x.Quiz.Id,
                Content = x.Content,
                IsDeleted = x.IsDeleted,
            });

            query = query.Where(c => c.IsDeleted == false);
            query = query.Where(c => c.QuizId == request.QuizId);
            var rlt = await query.ToListAsync();

            return new GetListQuestionByQuizIdDbResponse()
            {
                Data = new GetListQuestionByQuizIdDb
                {
                    Questions = rlt,
                },
                IsSuccess = rlt.Any()
            };
        }
    }
}
