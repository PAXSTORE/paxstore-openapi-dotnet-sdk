using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Paxstore.OpenApi.Help
{
    public class LongDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) {
                return null;
            }
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(String.Format("日期格式错误,got {0}.", reader.TokenType));
            }
            var ticks = (long)reader.Value;
            var date = new DateTime(1970, 1, 1);
            date = date.AddMilliseconds(ticks);
            
            return date;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime)
            {
                var epoc = new DateTime(1970, 1, 1);
                var delta = ((DateTime)value) - epoc;
                if (delta.TotalSeconds < 0)
                {
                    //throw new ArgumentOutOfRangeException("时间格式错误.1");
                    writer.WriteValue("");
                    return;
                }
                ticks = (long)delta.TotalMilliseconds;
                
            }
            else
            {
                throw new Exception("时间格式错误.2");
            }
            writer.WriteValue(ticks);
        }
    }
}
