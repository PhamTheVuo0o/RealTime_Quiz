using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppCore.Infrastructure.Common.Constants;
using AppCore.Infrastructure.Grpc.Contracts.Identity;
using AppCore.Infrastructure.Grpc.Contracts.Core;

namespace AppCore.Core.API.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class MonitorController : Controller
    {
        private IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;
        public MonitorController(IMediator mediator, IServiceProvider serviceProvider)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _serviceProvider = serviceProvider;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok(new
            {
                ServiceName = "CoreService",
                UpdateTime = CoreConstant.CORE_UPDATE_TIME
            });
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GrpcPingAll()
        {

            var itemMonitorCore = _serviceProvider.GetService<IMonitorCore>();

            var itemMonitorIdentity = _serviceProvider.GetService<IMonitorIdentity>();

            return Ok(new
            {
                core = await itemMonitorCore.PingAsync(),
                identity = await itemMonitorIdentity.PingAsync(),
            });
        }
    }
}
