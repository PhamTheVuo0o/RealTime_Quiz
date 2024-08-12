using AppCore.Infrastructure.Models;

namespace AppCore.Core.API.Application.Responses
{
    public class SubmitAnswerResponse : BaseResponse<SubmitAnswer>
    {
        public SubmitAnswerResponse() : base(new SubmitAnswer()) { }
    }
    public class SubmitAnswer
    {
        public int Core {  get; set; }
        public bool IsRight { get; set; }
        public SubmitAnswer()
        {
            IsRight = false;
        }
    }
}
