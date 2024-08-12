namespace AppCore.Core.Domain.Models
{
    public class QuizModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int LimitTimeInSecond { get; set; }
        
    }
}
