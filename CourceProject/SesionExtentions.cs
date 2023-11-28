using Newtonsoft.Json;

namespace CourceProject
{
    public static class SesionExtentions
    {
        //Запись произвольного объекта  в сессию
        public static void Set<T>(this ISession session, string key, T value)
        {
            var str = JsonConvert.SerializeObject(value);
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        //Считывание произвольного объекта из сессии
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
