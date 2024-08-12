using AutoMapper;
using AppCore.Identity.API.Application.Responses.Account;
using AppCore.Identity.Domain.Entities;

namespace AppCore.Identity.API.Application.Behavior
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, LoginResponse>();
            CreateMap<AppUser, GetCurrentUserProfile>();
        }
    }
}
