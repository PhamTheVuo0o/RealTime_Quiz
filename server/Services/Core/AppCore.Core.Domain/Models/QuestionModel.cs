namespace AppCore.Core.Domain.Models
{
    public class QuestionModel : BaseModel
    {
        public Guid QuizId { get; set; }
        public string Content { get; set; }
        public string Answer { get; set; }
    }
}
