using AppCore.Core.Infrastructure.IRepositories;
using AppCore.Infrastructure.Persistence.AppDbContext;
using AppCore.Infrastructure.Persistence.UnitOfWork;

namespace AppCore.Core.Infrastructure
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(IBaseDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider){
        }
        public IQuizRepository quizRepository => GetRepository<IQuizRepository>();
        public IQuestionRepository questionRepository => GetRepository<IQuestionRepository>();
        public IUserAnswerRepository userAnswerRepository => GetRepository<IUserAnswerRepository>();
    }
}
