using System;

namespace Carpeddit.Api.Models
{
    internal class JsonConverterAttribute : Attribute
    {
        private Type v;

        public JsonConverterAttribute(System.Type v)
        {
            this.v = v;
        }
    }
}