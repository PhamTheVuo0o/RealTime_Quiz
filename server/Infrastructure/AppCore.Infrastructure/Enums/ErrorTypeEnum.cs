namespace AppCore.Infrastructure.Enums
{
    public enum ErrorTypeEnum
    {
        None,
        DuplicateEmail,
        DuplicateName,
        DuplicateLocationCode,
        DuplicateWebsite,
        ManagerNotFound,
        OrganizationInvalid,
        OnlyCreatedExternalUser,
        CanNotGetTokenIssuedAt,
        CanNotGetTokenExpirationTime,
        ImageIsNullOrEmpty,
        ImageSizeIsBigerThanSizeLimit,
        UnSupportImageFormat,
        FailedToDecodeImage,
        CannotDeleteOldFileOnS3,
        NotExist
    }
}
