using System.Text.Json;

namespace ASPortStore.Infrastructure;

public static class SessionExtensions
{
    public static void SetJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetJson<T>(this ISession session, string key)
    {
        var sessionData = session.GetString(key);
        return (sessionData != null) ? JsonSerializer.Deserialize<T>(sessionData) : default;
    }
}
