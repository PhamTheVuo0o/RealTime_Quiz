using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AppCore.Identity.API.Application.Commands.Account;
using AppCore.Identity.API.Application.Responses.Account;
using IdentityModel;
using AppCore.Infrastructure.Extensions;

namespace AppCore.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result!=null ? (result.IsSuccess ? Ok(result) : BadRequest(result)) : Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GetCurrentUserProfileResponse>> GetCurrentUserProfile()
        {
            var result = await _mediator.Send(new GetCurrentUserProfileCommand()
            {
                Id = User.FindFirstValue(JwtClaimTypes.Id).ToGuid(),
                IssuedAt = User.FindFirstValue(JwtClaimTypes.IssuedAt),
                ExpirationTime = User.FindFirstValue(JwtClaimTypes.Expiration),
                Token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()
            });

            return result != null ? (result.IsSuccess ? Ok(result) : BadRequest(result)) : BadRequest();
        }
    }
}