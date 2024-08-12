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
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        DataContext _context;
        IHttpContextAccessor _httpContextAccessor;
        public UserAnswerRepository(DataContext context,
            IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SubmitAnswerDbResponse> SubmitAnswerDb(SubmitAnswerDbRequest request)
        {
            bool IsRight = request.IsRightWithCache;
            int Core = request.CurrentCore;
            
            if (!request.IsRightWithCache)
            {
                var query = _context.Quizs.SelectMany(x => x.Questions);

                query = query.Where(c => c.IsDeleted == false);
                query = query.Where(c => c.Quiz.Id == request.QuizId);
                query = query.Where(c => c.Id == request.QuestionId);
                query = query.Where(c => c.Answer == request.Answer);
                var rlt = await query.FirstOrDefaultAsync();
                IsRight = rlt != null;
            }

            if (IsRight)
            {
                Core ++;
                var query = _context.UserAnswers.Where(c => c.IsDeleted == false);
                query = query.Where(c => c.QuizId == request.QuizId);
                query = query.Where(c => c.UserId == request.UserId);
                var userAnswer = await query.FirstOrDefaultAsync();
                if (userAnswer != null)
                {
                    userAnswer.Core = Core;
                    userAnswer.LastAnswerTime = request.CurrentTimeSecond;
                    await UpdateAsync(userAnswer);
                }
                else {
                    userAnswer = new UserAnswer()
                    {
                        QuizId = request.QuizId,
                        UserId = request.UserId,
                        UserFullName = request.UserFullName,
                        Core = 1,
                        LastAnswerTime = request.CurrentTimeSecond,
                    };
                    await AddAsync(userAnswer);
                }
            }

            return new SubmitAnswerDbResponse()
            {
                Data = new SubmitAnswerDb()
                {
                    Core = Core,
                    IsRight = IsRight,
                },
                IsSuccess = true
            };
        }

        public async Task<GetListUserAnswerByQuizIdDbResponse> GetListUserAnswerByQuizIdDb(GetListUserAnswerByQuizIdDbRequest request)
        {
            var query = _context.UserAnswers.Select(x => new UserAnswerModel()
            {
                QuizId = x.QuizId,
                UserFullName = x.UserFullName,
                Core = x.Core,
                LastAnswerTime = x.LastAnswerTime,
            });

            query = query.Where(c => c.QuizId == request.QuizId);

            IOrderedQueryable<UserAnswerModel> orderedQuery = null;
            orderedQuery = query.OrderByDescending(e => EF.Property<object>(e, "Core"));
            orderedQuery = orderedQuery.ThenBy(e => EF.Property<object>(e, "LastAnswerTime"));

            var rlt = await orderedQuery.ToListAsync();
            

            return new GetListUserAnswerByQuizIdDbResponse()
            {
                 Data = new GetListUserAnswerByQuizIdDb()
                 {
                     Ranks = rlt,
                 },
                 IsSuccess = rlt.Any()
            };
        }
    }
}
