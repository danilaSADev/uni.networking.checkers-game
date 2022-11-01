using System;
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
            return JsonConvert.DeserializeObject<T>(Encoding.Unicode.GetString(data));
        }
    }
}