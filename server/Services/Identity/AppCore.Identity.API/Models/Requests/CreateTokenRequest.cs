using AppCore.Identity.Domain.Entities;

namespace AppCore.Identity.API.Models.Requests
{
    public class CreateTokenRequest
    {
        public AppUser User { get; set; }
        public List<string> Permissions { get; set; }
        public string Organization { get; set; }
        public bool IsRememberMe { get; set; }
        public bool IsUpdateTokenDetail { get; set; }
        public DateTime? IssuedAt { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public CreateTokenRequest()
        {
            IsRememberMe = false;
            IsUpdateTokenDetail = false;
            User = new AppUser();
            Permissions = new List<string>();
            Organization = "AppCore, Inc"; // Mock data (will implement Organization in feature)
            IssuedAt = DateTime.UtcNow;
            ExpirationTime = DateTime.UtcNow;
        }
    }
}
