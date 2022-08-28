using System.Text.Json;
using System.Threading.Tasks;

namespace GymLogger.App.Data;

public static class DeserializeExtensions
{
    private static JsonSerializerOptions defaultSerializerSettings = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public static T? Deserialize<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, defaultSerializerSettings);
    }

    public static T? Deserialize<T>(this Stream stream)
    {
        return JsonSerializer.Deserialize<T>(stream, defaultSerializerSettings);
    }

    public static ValueTask<T?> DeserializeAsync<T>(this Stream stream)
    {
        return JsonSerializer.DeserializeAsync<T>(stream, defaultSerializerSettings);
    }

    public static T? DeserializeCustom<T>(this string json, JsonSerializerOptions settings)
    {
        return JsonSerializer.Deserialize<T>(json, settings);
    }
}
