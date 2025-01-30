namespace POC_PayMob {
    public class PaymobOptions {
        public const string Paymob = "Paymob";
        public required  string authToken { get; set; }
        public required string PaymentTransactionURL { get; set; }
        public required string CaptureTransactionURL { get; set; }
        public required string VoidTransactionURL {  get; set; }
        public required string  HMACSecret { get; set; }
    }
}
