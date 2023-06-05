using System;

namespace Carpeddit.Api.Models
{
    internal class JsonPropertyNameAttribute : Attribute
    {
        private string v;

        public JsonPropertyNameAttribute(string v)
        {
            this.v = v;
        }
    }
}