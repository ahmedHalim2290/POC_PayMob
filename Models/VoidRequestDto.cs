using Newtonsoft.Json;
namespace POC_PayMob.Models {
    public class VoidRequestDto {
        [JsonProperty("transaction_id")]
        public int TransactionId { get; set; }
    }
}