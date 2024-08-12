namespace AppCore.Identity.API.Models.Responses
{
    public class CreateTokenResponse
    {
        public DateTime? IssuedAt { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public string Token { get; set; }
        public CreateTokenResponse()
        {
            IssuedAt = DateTime.UtcNow;
            ExpirationTime = DateTime.UtcNow;
            Token = string.Empty;
        }
    }
}
