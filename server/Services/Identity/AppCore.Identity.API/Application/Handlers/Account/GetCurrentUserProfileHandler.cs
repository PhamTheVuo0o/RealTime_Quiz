using AutoMapper;
using MediatR;
using AppCore.Identity.API.Application.Commands.Account;
using AppCore.Identity.API.Application.Responses.Account;
using AppCore.Identity.API.Models.Requests;
using AppCore.Identity.API.Services;
using AppCore.Identity.Infrastructure;
using AppCore.Identity.Domain.Enums;
using AppCore.Infrastructure.Extensions;
using AppCore.Infrastructure.RestClient;
using AppCore.Infrastructure.Enums;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Common;

namespace AppCore.Identity.API.Application.Handlers.Account
{
    public class GetCurrentUserProfileHandler : BaseClient<GetCurrentUserProfileResponse>, 
        IRequestHandler<GetCurrentUserProfileCommand, GetCurrentUserProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<AppSetting> _config;
        public GetCurrentUserProfileHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpClientFactory clientFactory,
            TokenService tokenService,
            IOptions<AppSetting> config,
            IServiceProvider serviceProvider) : base(clientFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _config = config;
            _serviceProvider = serviceProvider;
        }
        public async Task<GetCurrentUserProfileResponse> Handle(GetCurrentUserProfileCommand request, CancellationToken cancellationToken)
        {
            GetCurrentUserProfileResponse getCurrentUserProfileResponse = new GetCurrentUserProfileResponse();
            var response = new GetCurrentUserProfileResponse();

            var user = await _unitOfWork.appUserRepository.GetAsync(x => x.Id == request.Id);
            if (user != null)
            {
                if(user.Status == UserStatusEnum.Active.ToShort())
                {
                    getCurrentUserProfileResponse.Data = _mapper.Map<GetCurrentUserProfile>(user);
                    getCurrentUserProfileResponse.IsSuccess = true;
                }
                else
                {
                    getCurrentUserProfileResponse.Data.IsNotActive = true;
                }
            }
            return getCurrentUserProfileResponse;
        }
    }
}
