using AppCore.Infrastructure.Persistence.Entities;

namespace AppCore.Core.Domain.Entities
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int LimitTimeInSecond { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
