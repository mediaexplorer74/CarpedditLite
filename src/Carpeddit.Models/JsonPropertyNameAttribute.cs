//using System.Text.Json.Nodes;
//using System.Text.Json.Serialization;

using System;

namespace Carpeddit.Models.Api
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