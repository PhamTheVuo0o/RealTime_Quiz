using AppCore.Core.API.Application.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Core.API.Application.Commands
{
    public class GetListQuestionByQuizIdCommand : IRequest<GetListQuestionByQuizIdResponse>
    {
        [Required]
        public Guid QuizId { get; set; }
    }
}
