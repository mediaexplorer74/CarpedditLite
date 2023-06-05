using Carpeddit.Api.Helpers;
using Carpeddit.Api.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Utf8Json;
//using System.Text.Json.Serialization;

namespace Carpeddit.Api
{
    /*
    [JsonSerializable(typeof(More))]
    [JsonSerializable(typeof(Message))]
    [JsonSerializable(typeof(Post))]
    [JsonSerializable(typeof(Comment))]
    [JsonSerializable(typeof(User))]
    [JsonSerializable(typeof(IDictionary<string, string>))]
    [JsonSerializable(typeof(TokenInfo))]
    [JsonSerializable(typeof(IList<Listing<IList<ApiObjectWithKind<Comment>>>>))]
    [JsonSerializable(typeof(Listing<IList<ApiObjectWithKind<Post>>>))]
    [JsonSerializable(typeof(Listing<IList<ApiObjectWithKind<Message>>>))]
    [JsonSerializable(typeof(ApiObjectWithKind<Subreddit>))]
    [JsonSerializable(typeof(ApiObjectWithKind<User>))]
    */
    public sealed partial class ApiJsonContext : JsonSerializerContext
    {
        public static class Default
        {
            internal static IJsonFormatterResolver TokenInfo;
            internal static JsonTypeInfo<List<HttpResponseMessage>> IListListingIListApiObjectWithKindComment;
            internal static JsonTypeInfo<User> User;
            internal static JsonTypeInfo<IList> ApiObjectWithKindSubreddit;
            internal static JsonTypeInfo<object> ListingIListApiObjectWithKindPost;
            internal static JsonTypeInfo<object> ListingIListApiObjectWithKindMessage;

            public static JsonTypeInfo<object> ApiObjectWithKindUser { get; internal set; }
        }
    }
}
