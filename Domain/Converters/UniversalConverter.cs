using System.Text;
using Newtonsoft.Json;

namespace Domain.Converters
{
    public class UniversalConverter
    {
        public static byte[] ConvertObject<T>(T obj)
        {
            return Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public static T ConvertBytes<T>(byte[] data)
        {
            var str = Encoding.Unicode.GetString(data).Normalize();
            var end = str.LastIndexOf("}") + 1;
            str = str.Remove(end, str.Length - end);
            var value = JsonConvert.DeserializeObject<T>(str);
            return value;
        }
    }
}