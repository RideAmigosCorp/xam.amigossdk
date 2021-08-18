using Newtonsoft.Json;

namespace AmigosSDK.Shared
{
    [JsonObject]
    public class ActionEvent
    {

        [JsonProperty("action", Required = Required.Always)]
        public string Action { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

    }
}
