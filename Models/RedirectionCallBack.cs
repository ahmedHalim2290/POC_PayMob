using Newtonsoft.Json;

namespace POC_PayMob.Models {
    public class CallBackTransaction {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("pending")]
        public string Pending { get; set; }

        [JsonProperty("amount_cents")]
        public string AmountCents { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("is_auth")]
        public string IsAuth { get; set; }

        [JsonProperty("is_capture")]
        public string IsCapture { get; set; }

        [JsonProperty("is_standalone_payment")]
        public string IsStandalonePayment { get; set; }

        [JsonProperty("is_voided")]
        public string IsVoided { get; set; }

        [JsonProperty("is_refunded")]
        public string IsRefunded { get; set; }

        [JsonProperty("is_3d_secure")]
        public string Is3DSecure { get; set; }

        [JsonProperty("integration_id")]
        public string IntegrationId { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("has_parent_transaction")]
        public string HasParentTransaction { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("merchant_commission")]
        public string MerchantCommission { get; set; }

        [JsonProperty("discount_details")]
        public string DiscountDetails { get; set; }

        [JsonProperty("is_void")]
        public string IsVoid { get; set; }

        [JsonProperty("is_refund")]
        public string IsRefund { get; set; }

        [JsonProperty("error_occured")]
        public string ErrorOccured { get; set; }

        [JsonProperty("refunded_amount_cents")]
        public string RefundedAmountCents { get; set; }

        [JsonProperty("captured_amount")]
        public string CapturedAmount { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("is_settled")]
        public string IsSettled { get; set; }

        [JsonProperty("bill_balanced")]
        public string BillBalanced { get; set; }

        [JsonProperty("is_bill")]
        public string IsBill { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("data.message")]
        public string DataMessage { get; set; }

        [JsonProperty("source_data.type")]
        public string SourceDataType { get; set; }

        [JsonProperty("source_data.pan")]
        public string SourceDataPan { get; set; }

        [JsonProperty("source_data.sub_type")]
        public string SourceDataSubType { get; set; }

        [JsonProperty("acq_response_code")]
        public string AcqResponseCode { get; set; }

        [JsonProperty("txn_response_code")]
        public string TxnResponseCode { get; set; }

        [JsonProperty("hmac")]
        public string Hmac { get; set; }
    }
}

