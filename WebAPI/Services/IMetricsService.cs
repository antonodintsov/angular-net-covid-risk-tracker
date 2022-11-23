namespace WebAPI.Services
{
    public interface IMetricsService<T>
    {
        Task<List<T>> ReadStatesDataAsync();
    }
}
