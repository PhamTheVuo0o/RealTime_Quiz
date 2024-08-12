namespace AppCore.Core.Domain.Models
{
    public class UserAnswerModel
    {
        public Guid QuizId { get; set; }
        public string UserFullName { get; set; }
        public int Core { get; set; }
        public int LastAnswerTime { get; set; }

    }
}
