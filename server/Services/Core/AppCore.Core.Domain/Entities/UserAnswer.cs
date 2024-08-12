using AppCore.Infrastructure.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Core.Domain.Entities
{
    public class UserAnswer : BaseEntity
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public int Core { get; set; }
        public int LastAnswerTime { get; set; }
        
    }
}
