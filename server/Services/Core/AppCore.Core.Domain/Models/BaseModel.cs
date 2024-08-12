namespace AppCore.Core.Domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public short Status { get; set; }
    }
}
