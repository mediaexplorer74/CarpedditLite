//using System.Text.Json.Serialization;

using Carpeddit.Models.Api;

namespace Carpeddit.Models
{
    public sealed class PasswordToken
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
