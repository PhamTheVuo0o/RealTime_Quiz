using Microsoft.AspNetCore.Identity;
using AppCore.Infrastructure.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Identity.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public override Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? OTPToken { get; set; }
        public DateTimeOffset ExpiredTime { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public short Status { get; set; }
        public string? Token { get; set; }
        public short UserType { get; set; }
        public Guid? BaseOrganizationId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastUpdatedDate { get; set; }
        public string? LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public string? Author { get; set; }
    }
}
