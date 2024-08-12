using AppCore.Core.API.Application.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppCore.Core.API.Application.Commands
{
    public class SubmitAnswerCommand : IRequest<SubmitAnswerResponse>
    {
        [JsonIgnore]
        public Guid? UserId { get; set; }
        [Required]
        public Guid QuizId { get; set; }
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public string UserFullName { get; set; }
        [Required]
        public string Answer {  get; set; }

        [JsonIgnore]
        public bool? IsRightWithCache { get; set; }
        [Required]
        public int CurrentCore { get; set; }
        [Required]
        public int CurrentTimeSecond { get; set; }
    }
}
