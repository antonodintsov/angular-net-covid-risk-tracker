namespace WebAPI.Converters
{
    public interface IDataConverter
    {
        Task<T> Convert<T>(Stream stream) where T : new();
    }
}
