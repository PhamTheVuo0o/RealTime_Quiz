namespace AppCore.Infrastructure.RestClient
{
    public class BaseClient<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;
        public BaseClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
