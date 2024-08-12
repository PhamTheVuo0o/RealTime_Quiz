using AppCore.Core.Infrastructure.IRepositories;
using AppCore.Infrastructure.Persistence.UnitOfWork;

namespace AppCore.Core.Infrastructure
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IQuizRepository quizRepository { get; }
        IQuestionRepository questionRepository { get; }
        IUserAnswerRepository userAnswerRepository { get; }
    }
}
