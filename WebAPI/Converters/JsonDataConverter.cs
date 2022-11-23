using System.Text.Json;

namespace WebAPI.Converters
{
    public class JsonDataConverter : IDataConverter
    {
        public async Task<T> Convert<T>(Stream stream) where T : new()
        {
            return await JsonSerializer.DeserializeAsync<T>(stream) ?? new T();
        }
    }
}
