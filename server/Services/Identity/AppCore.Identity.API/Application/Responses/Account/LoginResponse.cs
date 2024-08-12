using AppCore.Infrastructure.Models;

namespace AppCore.Identity.API.Application.Responses.Account
{
    public class LoginResponse : BaseResponse<Login>
    {
        public LoginResponse() : base (new Login())
        {
        }
    }
    public class Login
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public bool IsNotActive { get; set; }
        public Login()
        {
            IsNotActive = false;
            Token = string.Empty;
            Email = string.Empty;
        }
    }
}
