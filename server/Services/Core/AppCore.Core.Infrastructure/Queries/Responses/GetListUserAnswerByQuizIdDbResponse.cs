using AppCore.Core.Domain.Models;
using AppCore.Infrastructure.Models;

namespace AppCore.Core.Infrastructure.Queries.Responses
{
    public class GetListUserAnswerByQuizIdDbResponse : BaseResponse<GetListUserAnswerByQuizIdDb>
    {
        public GetListUserAnswerByQuizIdDbResponse() : base(new GetListUserAnswerByQuizIdDb()) { }
    }
    public class GetListUserAnswerByQuizIdDb
    {
        public List<UserAnswerModel> Ranks { get; set; }
        public GetListUserAnswerByQuizIdDb()
        {
            Ranks = new List<UserAnswerModel>();
        }
    }
}
