using AppCore.Core.Domain.Models;
using AppCore.Infrastructure.Models;

namespace AppCore.Core.API.Application.Responses
{
    public class GetListQuestionByQuizIdResponse : BaseResponse<GetListQuestionByQuizId>
    {
        public GetListQuestionByQuizIdResponse() : base(new GetListQuestionByQuizId()) { }
    }
    public class GetListQuestionByQuizId
    {
        public List<QuestionModel> Questions { get; set; }
        public GetListQuestionByQuizId() {
            Questions = new List<QuestionModel>(); 
        }
    }
}
