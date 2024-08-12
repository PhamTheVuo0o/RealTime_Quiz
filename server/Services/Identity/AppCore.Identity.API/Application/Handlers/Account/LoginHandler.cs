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

namespace AppCore.Identity.API.Application.Handlers.Account
{
    public class LoginHandler : BaseClient<LoginResponse>, 
        IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly IServiceProvider _serviceProvider;
        public LoginHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            TokenService tokenService,
            IServiceProvider serviceProvider,
            IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _serviceProvider = serviceProvider;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginResponse loginResponse = new LoginResponse();
            var user = await _unitOfWork.appUserRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                var result = await _unitOfWork.appUserRepository.CheckPasswordAsync(user, request.Password);

                if (result)
                {
                    if (user.Status == UserStatusEnum.Active.ToShort())
                    {
                        var permission = new List<string>(); // Implement return permission here
                        if (user.BaseOrganizationId != null)
                        {
                            var createTokenResponse = _tokenService.CreateToken(new CreateTokenRequest()
                            {
                                User = user,
                                Permissions = permission,
                                IsRememberMe = request.IsRememberMe,
                            });
                            if (createTokenResponse != null && !string.IsNullOrEmpty(createTokenResponse.Token))
                            {
                                loginResponse = _mapper.Map<LoginResponse>(user);
                                loginResponse.Data.Email = request.Email;
                                loginResponse.Data.Token = createTokenResponse.Token;
                                loginResponse.IsSuccess = true;
                                user.LastLoginDate = DateTime.UtcNow;
                                user.Token = createTokenResponse.Token;
                                await _unitOfWork.appUserRepository.UpdateAsync(user);
                            }
                        }
                    }
                    else
                    {
                        loginResponse.Data.IsNotActive = true;
                    }
                }
            }
            return loginResponse;
        }

        #region Private Fucntion
        #endregion
    }
}
