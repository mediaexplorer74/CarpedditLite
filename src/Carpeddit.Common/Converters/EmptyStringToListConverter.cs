using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utf8Json;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace Carpeddit.Common.Converters
{
    public abstract class EmptyStringToListConverter<T> : JsonConverter<IEnumerable<T>>
    {
    }
}
