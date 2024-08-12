using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Infrastructure.Persistence.Entities
{
    public interface IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? BaseOrganizationId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? LastUpdatedDate { get; set; }

        public string? LastUpdatedBy { get; set; }
        
        public bool IsDeleted { get; set; }

        public short Status { get; set; }

        [NotMapped]
        public string? Author { get; set; }
    }
}
