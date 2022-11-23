namespace WebAPI.Services
{
    public class CovidActNowSettings
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string StatesDataRequest { get; set; }
        public TimeSpan CacheLifetime { get; set; }
    }
}
