using AppCore.Core.Domain.Models;
using AppCore.Infrastructure.Models;

namespace AppCore.Core.Infrastructure.Queries.Responses
{
    public class GetListQuestionByQuizIdDbResponse : BaseResponse<GetListQuestionByQuizIdDb>
    {
        public GetListQuestionByQuizIdDbResponse() : base(new GetListQuestionByQuizIdDb()) { }
    }
    public class GetListQuestionByQuizIdDb
    {
        public List<QuestionModel> Questions { get; set; }
        public GetListQuestionByQuizIdDb()
        {
            Questions = new List<QuestionModel>();
        }
    }
}
