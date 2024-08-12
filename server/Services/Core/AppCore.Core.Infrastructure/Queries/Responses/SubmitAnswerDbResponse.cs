using AppCore.Infrastructure.Models;

namespace AppCore.Core.Infrastructure.Queries.Responses
{
    public class SubmitAnswerDbResponse : BaseResponse<SubmitAnswerDb>
    {
        public SubmitAnswerDbResponse() : base(new SubmitAnswerDb()) { }
    }
    public class SubmitAnswerDb
    {
        public int Core { get; set; }
        public bool IsRight { get; set; }
        public SubmitAnswerDb()
        {
            IsRight = false;
        }
    }
}
