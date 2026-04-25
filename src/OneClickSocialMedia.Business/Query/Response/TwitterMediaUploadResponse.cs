using Newtonsoft.Json;

namespace OneClickSocialMedia.Business.Query.Response
{
    public class TwitterMediaUploadResponse : Response
    {
        [JsonProperty("media_id")]
        public long MediaId { get; set; }

        [JsonProperty("media_id_string")]
        public string MediaIdString { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("expires_after_secs")]
        public int ExpiresAfterSeconds { get; set; }
    }
}
