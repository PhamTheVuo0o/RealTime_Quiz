using AppCore.Core.API.Application.Responses;
using MediatR;

namespace AppCore.Core.API.Application.Commands
{
    public class GetListQuizCommand : IRequest<GetListQuizResponse>
    {
    }
}
