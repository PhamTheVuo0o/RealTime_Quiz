using AppCore.Infrastructure.Models;
using AppCore.Identity.Domain.Enums;
using System.Drawing.Printing;

namespace AppCore.Identity.API.Application.Responses.Account
{
    public class GetCurrentUserProfileResponse : BaseResponse<GetCurrentUserProfile>
    {

        public GetCurrentUserProfileResponse() : base(new GetCurrentUserProfile())
        {
        }
    }
    public class GetCurrentUserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsNotActive { get; set; }

        public GetCurrentUserProfile()
        {
            IsNotActive = false;
            FirstName = string.Empty;
            LastName = string.Empty;
            Avatar = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}
