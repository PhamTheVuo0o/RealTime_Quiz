using MediatR;
using AppCore.Identity.API.Application.Responses.Account;
using System.Text.Json.Serialization;

namespace AppCore.Identity.API.Application.Commands.Account
{
    public class GetCurrentUserProfileCommand : IRequest<GetCurrentUserProfileResponse>
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        [JsonIgnore]
        public string? Token { get; set; }
        [JsonIgnore]
        public string? IssuedAt { get; set; }
        [JsonIgnore]
        public string? ExpirationTime { get; set; }
        public GetCurrentUserProfileCommand()
        {
            Id = Guid.NewGuid();
            IssuedAt = string.Empty;
            ExpirationTime = string.Empty;
        }
    }
}