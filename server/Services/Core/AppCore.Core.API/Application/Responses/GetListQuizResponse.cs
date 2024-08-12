using AppCore.Core.Domain.Models;
using AppCore.Infrastructure.Models;

namespace AppCore.Core.API.Application.Responses
{
    public class GetListQuizResponse : BaseResponse<GetListQuiz>
    {
        public GetListQuizResponse() : base(new GetListQuiz()) { }
    }
    public class GetListQuiz
    {
        public List<QuizModel> Quizs { get; set; }
        public GetListQuiz()
        {
            Quizs = new List<QuizModel>();
        }
    }
}
