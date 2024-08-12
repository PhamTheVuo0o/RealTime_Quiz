using AppCore.Core.Domain.Models;
using AppCore.Infrastructure.Models;

namespace AppCore.Core.Infrastructure.Queries.Responses
{
    public class GetListQuizDbResponse : BaseResponse<GetListQuizDb>
    {
        public GetListQuizDbResponse() : base(new GetListQuizDb()) { }
    }
    public class GetListQuizDb
    {
        public List<QuizModel> Quizs { get; set; }
        public GetListQuizDb()
        {
            Quizs = new List<QuizModel>();
        }
    }
}
