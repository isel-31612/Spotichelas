using Newtonsoft.Json;
namespace SpotifyBridge
{
    public class JsonSearchInfo
    {
        [JsonProperty("num_results")]
        public int Count;
        [JsonProperty("limit")]
        public int Max;
        [JsonProperty("offset")]
        public int Offset;
        [JsonProperty("query")]
        public string Query;
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("page")]
        public int Page;

        public JsonSearchInfo()
        { }
    }
}
