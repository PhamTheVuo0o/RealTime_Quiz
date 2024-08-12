using AppCore.Core.Domain.Entities;
using AppCore.Core.Domain.Models;
using AppCore.Core.Infrastructure.IRepositories;
using AppCore.Core.Infrastructure.Queries.Responses;
using AppCore.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Core.Infrastructure.Repositories
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        DataContext _context;
        IHttpContextAccessor _httpContextAccessor;
        public QuizRepository(DataContext context,
            IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetListQuizDbResponse> GetListQuizDb()
        {
            var query = _context.Quizs.Select(x => new QuizModel()
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                LimitTimeInSecond = x.LimitTimeInSecond,
                IsDeleted = x.IsDeleted,

            });

            query = query.Where(c => c.IsDeleted == false);
            query = query.Where(c => c.StartTime <= DateTime.UtcNow);
            query = query.Where(c => c.EndTime > DateTime.UtcNow);

            IOrderedQueryable<QuizModel> orderedQuery = null;
            orderedQuery = query.OrderBy(e => EF.Property<object>(e, "Name"));

            var rlt = await orderedQuery.ToListAsync();

            return new GetListQuizDbResponse() { 
                Data = new GetListQuizDb
                {
                    Quizs = rlt,
                },
                IsSuccess = rlt.Any()
            };
        }
    }
}
