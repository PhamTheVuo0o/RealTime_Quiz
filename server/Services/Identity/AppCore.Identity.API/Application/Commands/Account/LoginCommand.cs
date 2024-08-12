using MediatR;
using AppCore.Identity.API.Application.Responses.Account;
using System.ComponentModel.DataAnnotations;
namespace AppCore.Identity.API.Application.Commands.Account
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
        public LoginCommand()
        {
            IsRememberMe = false;
            Password = string.Empty;
            Email = string.Empty;
        }
    }
}