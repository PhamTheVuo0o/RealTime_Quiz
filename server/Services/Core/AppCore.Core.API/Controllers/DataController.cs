using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using IdentityModel;
using AppCore.Infrastructure.Extensions;
using AppCore.Core.API.Application.Responses;
using AppCore.Core.API.Application.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppCore.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GetListQuestionByQuizIdResponse>> GetListQuestionByQuizId(GetListQuestionByQuizIdCommand command)
        {
            var result = await _mediator.Send(command);
            return result!=null ? (result.IsSuccess ? Ok(result) : BadRequest(result)) : Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GetListQuizResponse>> GetListQuiz()
        {
            var result = await _mediator.Send(new GetListQuizCommand());
            return result != null ? (result.IsSuccess ? Ok(result) : BadRequest(result)) : Unauthorized();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SubmitAnswerResponse>> SubmitAnswer(SubmitAnswerCommand command)
        {
            command.UserId = User.FindFirstValue(JwtClaimTypes.Id).ToGuid();
            var result = await _mediator.Send(command);
            return result != null ? (result.IsSuccess ? Ok(result) : BadRequest(result)) : Unauthorized();
        }
    }
}