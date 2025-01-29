using Newtonsoft.Json;
namespace POC_PayMob.Models {

    public class CaptureRequestDto {
        [JsonProperty("transaction_id")]
        public int TransactionId { get; set; }

        [JsonProperty("amount_cents")]
        public decimal AmountCents { get; set; }
    }
}
