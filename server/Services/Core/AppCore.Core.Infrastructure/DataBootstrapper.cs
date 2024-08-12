using AppCore.Core.Infrastructure.IRepositories;
using AppCore.Core.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AppCore.Core.Infrastructure
{
    public static class DataBootstrapper
    {
        public static void AddDataBootstrapper(this IServiceCollection services)
        {
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuizRepository, QuizRepository>();
            services.AddTransient<IUserAnswerRepository, UserAnswerRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
