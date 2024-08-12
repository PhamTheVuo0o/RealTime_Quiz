using AppCore.Identity.Domain.Entities;
using AppCore.Identity.Domain.Enums;
using AppCore.Infrastructure.Common.Constants;
using AppCore.Infrastructure.Extensions;
using System.Globalization;

namespace AppCore.Identity.Infrastructure.SeedWork
{
    public static class SeedInitialAppUser
    {
        public static async Task SeedData(DataContext context,
            IUnitOfWork unitOfWork)
        {
            var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = Guid.Parse("f95a290b-92d7-4cf5-a63a-70eb3a0b59fc"),
                        FirstName = "A",
                        LastName = "User",
                        Avatar = "/images/avatar/1.svg",
                        Email = "A@AppCore.com",
                        UserName = "A@AppCore.com",
                        UserType = UserTypeEnum.Identity.ToShort(),
                        Status = UserStatusEnum.Active.ToShort(),
                        BaseOrganizationId = new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e"),
                        LastUpdatedDate = DateTime.ParseExact("2024-09-07 23:59:59", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    },
                    new AppUser
                    {
                        Id = Guid.Parse("d04c247e-d0b9-462a-aa28-9914d0b24d7f"),
                        FirstName = "B",
                        LastName = "User",
                        Avatar = "/images/avatar/2.svg",
                        Email = "B@AppCore.com",
                        UserName = "B@AppCore.com",
                        UserType = UserTypeEnum.Identity.ToShort(),
                        Status = UserStatusEnum.Active.ToShort(),
                        BaseOrganizationId = new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e"),
                        LastUpdatedDate = DateTime.ParseExact("2024-09-07 23:59:59", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    },
                    new AppUser
                    {
                        Id = Guid.Parse("017c34f9-12ee-49ae-93b7-a0eee5bf8863"),
                        FirstName = "C",
                        LastName = "User",
                        Avatar = "/images/avatar/3.svg",
                        Email = "C@AppCore.com",
                        UserName = "C@AppCore.com",
                        UserType = UserTypeEnum.Identity.ToShort(),
                        Status = UserStatusEnum.Active.ToShort(),
                        BaseOrganizationId = new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e"),
                        LastUpdatedDate = DateTime.ParseExact("2024-09-07 23:59:59", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    },
                    new AppUser
                    {
                        Id = Guid.Parse("44d9361e-ca01-423c-ad25-a81bafc29ecb"),
                        FirstName = "D",
                        LastName = "User",
                        Avatar = "/images/avatar/4.svg",
                        Email = "D@AppCore.com",
                        UserName = "D@AppCore.com",
                        UserType = UserTypeEnum.Identity.ToShort(),
                        Status = UserStatusEnum.Active.ToShort(),
                        BaseOrganizationId = new Guid("fa1b265b-e660-4cac-b2ba-ab89b340512e"),
                        LastUpdatedDate = DateTime.ParseExact("2024-09-07 23:59:59", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    },
                    new AppUser
                    {
                        Id = Guid.Parse("69c43945-51de-4f97-b2c8-a7733039e4cc"),
                        FirstName = "E",
                        LastName = "User",
                        Avatar = "/images/avatar/5.svg",
                        Email = "E@AppCore.com",
                        UserName = "E@AppCore.com",
                        UserType = UserTypeEnum.Identity.ToShort(),
                        Status = UserStatusEnum.Active.ToShort(),
                        BaseOrganizationId = new Guid("3c3db6c8-e660-4cac-89ee-338b4c5371e8"),
                        LastUpdatedDate = DateTime.ParseExact("2024-05-10 23:59:59", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    },
                    new AppUser
                    {
                        Id = Guid.Parse("c8f90372-08d3-4cc4-be00-58be69e2eb0b"),
                        FirstName = "F",
                        LastName = "User",
                        Avatar = "/images/avatar/6.svg",
                        Email = "F@AppCore.com",
                        UserName = "F@AppCore.com",
                        UserType = UserTypeEnum.Identity.ToShort(),
                        Status = UserStatusEnum.Active.ToShort(),
                        BaseOrganizationId = new Guid("3c3db6c8-e660-4cac-89ee-338b4c5371e8"),
                        LastUpdatedDate = DateTime.ParseExact("2024-05-10 23:59:59", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    },
                };
            if (!unitOfWork.appUserRepository.GetUserManager().Users.Any())
            {
                
                foreach (var item in users)
                {
                    item.LastUpdatedDate = null;
                    await unitOfWork.appUserRepository.GetUserManager().CreateAsync(item, CoreConstant.PASSWORD_DEFAULT);
                }
                await context.SaveChangesAsync();
            }
            else
            {
                await UpdateData(context, unitOfWork, users);
            }
        }
        private static async Task UpdateData(DataContext context,
            IUnitOfWork unitOfWork,
            List<AppUser> users)
        {
            foreach (var item in users)
            {
                if (item.LastUpdatedDate >= DateTime.UtcNow)
                {
                    item.LastUpdatedDate = null;
                    await unitOfWork.appUserRepository.AddOrUpdateUserAsync(item);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
