using Newtonsoft.Json;

namespace POC_PayMob.Models {
    public class AuthRequestDto {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
    }
}
