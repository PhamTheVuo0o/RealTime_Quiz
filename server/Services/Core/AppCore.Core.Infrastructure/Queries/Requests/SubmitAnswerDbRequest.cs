namespace AppCore.Core.Infrastructure.Queries.Requests
{
    public class SubmitAnswerDbRequest
    {
        public Guid UserId { get; set; }
        public Guid QuizId { get; set; }
        public Guid QuestionId { get; set; }
        public string UserFullName { get; set; }
        public string Answer { get; set; }
        public bool IsRightWithCache { get; set; }
        public int CurrentCore { get; set; }
        public int CurrentTimeSecond { get; set; }
    }
}
