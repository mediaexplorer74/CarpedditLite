//using System.Text.Json.Nodes;
//using System.Text.Json.Serialization;

using Windows.Data.Json;

namespace Carpeddit.Models.Api
{
    public sealed class CommentContributionSettings
    {
        [JsonPropertyName("allowed_media_types")]
        public JsonArray? AllowedMediaTypes { get; set; }
    }
}
