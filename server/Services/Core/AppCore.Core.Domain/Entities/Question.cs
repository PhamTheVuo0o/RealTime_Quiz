using AppCore.Infrastructure.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace AppCore.Core.Domain.Entities
{
    public class Question : BaseEntity
    {
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
        public string Content { get; set; }
        public string Answer { get; set; }
    }
}
