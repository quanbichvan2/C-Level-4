using Newtonsoft.Json;

namespace Lab_8_2_Net_4.Helpers
{
    // Lớp này cung cấp các phương thức để lưu trữ và lấy dữ liệu từ session của ứng dụng dưới dạng JSON.
    public static class SessionNJsonHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T? GetObjectAsJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
