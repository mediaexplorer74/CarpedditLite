using System;
using System.Text;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace Carpeddit.Common.Converters
{
    public abstract class BaseTimestampConverter : JsonConverter<DateTime>
    {
        public abstract long ConvertToSeconds(DateTime dateTime);

        public abstract DateTime ParseDateFromSeconds(long seconds);
    }
}
