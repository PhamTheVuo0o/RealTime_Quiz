using AppCore.Infrastructure.Persistence.Enums;
namespace AppCore.Identity.Domain.Enums
{
    public enum UserTypeEnum
    {
        Unknown = BaseTypeEnum.Unknown,
        Identity = BaseTypeEnum.Identity,
        Google,
        GitHub,
        Linkedin,
        Facebook
    }
}
