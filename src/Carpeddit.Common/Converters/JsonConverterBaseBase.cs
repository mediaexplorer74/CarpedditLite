using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utf8Json;

namespace Carpeddit.Common.Converters
{
    public class JsonConverterBaseBase
    {
        /*
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valueString = Encoding.UTF8.GetString(reader.ValueSpan.ToArray());

            if (!string.IsNullOrEmpty(valueString) && valueString != "null" && !bool.TryParse(valueString, out _))
            {
                if (DateTime.TryParse(valueString, out DateTime parsedDate))
                    return parsedDate;

                return ParseDateFromSeconds((long)Convert.ToDouble(valueString));
            }

            return default;
        }
        */

        /*
        public override IEnumerable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, 
            JsonSerializerOptions options)
        {
            var str = reader.GetString();

            if (string.IsNullOrEmpty(str))
                return Enumerable.Empty<T>();

            return JsonSerializer.Deserialize<IEnumerable<T>>(Encoding.UTF8.GetString(
                reader.ValueSpan.ToArray()));
        }
        */
        /*
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value is DateTime date && date != default)
            {
                writer.WriteRawValue(ConvertToSeconds(date).ToString());
                return;
            }

            writer.WriteNullValue();
        }
        */
    }
}