//namespace POC_PayMob.Models
//{
//    public class PayMobResponseModel {
//        public string? Type { get; set; }
//        public Transaction? Obj { get; set; }
//        public int AcceptFees { get; set; }
//        public string? IssuerBank { get; set; }
//        public string? TransactionProcessedCallbackResponses { get; set; }
//    }

//    public class Transaction {
//        public int Id { get; set; }
//        public bool Pending { get; set; }
//        public int AmountCents { get; set; }
//        public bool Success { get; set; }
//        public bool IsAuth { get; set; }
//        public bool IsCapture { get; set; }
//        public bool IsStandalonePayment { get; set; }
//        public bool IsVoided { get; set; }
//        public bool IsRefunded { get; set; }
//        public bool Is3Dsecure { get; set; }
//        public int IntegrationId { get; set; }
//        public int ProfileId { get; set; }
//        public bool HasParentTransaction { get; set; }
//        public Order? Order { get; set; }
//        public DateTime CreatedAt { get; set; }
//        public List<object>? TransactionProcessedCallbackResponses { get; set; }
//        public string? Currency { get; set; }
//        public SourceData? SourceData { get; set; }
//        public string? ApiSource { get; set; }
//        public string? TerminalId { get; set; }
//        public int MerchantCommission { get; set; }
//        public object? Installment { get; set; }
//        public List<object>? DiscountDetails { get; set; }
//        public bool IsVoid { get; set; }
//        public bool IsRefund { get; set; }
//        public Data? Data { get; set; }
//        public bool IsHidden { get; set; }
//        public PaymentKeyClaims? PaymentKeyClaims { get; set; }
//        public bool ErrorOccured { get; set; }
//        public bool IsLive { get; set; }
//        public object? OtherEndpointReference { get; set; }
//        public int RefundedAmountCents { get; set; }
//        public int SourceId { get; set; }
//        public bool IsCaptured { get; set; }
//        public int CapturedAmount { get; set; }
//        public object? MerchantStaffTag { get; set; }
//        public DateTime UpdatedAt { get; set; }
//        public bool IsSettled { get; set; }
//        public bool BillBalanced { get; set; }
//        public bool IsBill { get; set; }
//        public int Owner { get; set; }
//        public object? ParentTransaction { get; set; }
//    }

//    public class Order {
//        public int Id { get; set; }
//        public DateTime CreatedAt { get; set; }
//        public bool DeliveryNeeded { get; set; }
//        public Merchant? Merchant { get; set; }
//        public object? Collector { get; set; }
//        public int AmountCents { get; set; }
//        public ShippingData? ShippingData { get; set; }
//        public string? Currency { get; set; }
//        public bool IsPaymentLocked { get; set; }
//        public bool IsReturn { get; set; }
//        public bool IsCancel { get; set; }
//        public bool IsReturned { get; set; }
//        public bool IsCanceled { get; set; }
//        public object? MerchantOrderId { get; set; }
//        public object? WalletNotification { get; set; }
//        public int PaidAmountCents { get; set; }
//        public bool NotifyUserWithEmail { get; set; }
//        public List<object>? Items { get; set; }
//        public string? OrderUrl { get; set; }
//        public int CommissionFees { get; set; }
//        public int DeliveryFeesCents { get; set; }
//        public int DeliveryVatCents { get; set; }
//        public string? PaymentMethod { get; set; }
//        public object? MerchantStaffTag { get; set; }
//        public string? ApiSource { get; set; }
//        public Dictionary<string, object?>? Data { get; set; }
//    }

//    public class Merchant {
//        public int Id { get; set; }
//        public DateTime CreatedAt { get; set; }
//        public List<string>? Phones { get; set; }
//        public List<string>? CompanyEmails { get; set; }
//        public string? CompanyName { get; set; }
//        public string? State { get; set; }
//        public string? Country { get; set; }
//        public string? City { get; set; }
//        public string? PostalCode { get; set; }
//        public string? Street { get; set; }
//    }

//    public class ShippingData {
//        public int Id { get; set; }
//        public string? FirstName { get; set; }
//        public string? LastName { get; set; }
//        public string? Street { get; set; }
//        public string? Building { get; set; }
//        public string? Floor { get; set; }
//        public string? Apartment { get; set; }
//        public string? City { get; set; }
//        public string? State { get; set; }
//        public string? Country { get; set; }
//        public string? Email { get; set; }
//        public string? PhoneNumber { get; set; }
//        public string? PostalCode { get; set; }
//        public string? ExtraDescription { get; set; }
//        public string? ShippingMethod { get; set; }
//        public int OrderId { get; set; }
//        public int Order { get; set; }
//    }

//    public class SourceData {
//        public string? Pan { get; set; }
//        public string? Type { get; set; }
//        public object? Tenure { get; set; }
//        public string? SubType { get; set; }
//    }

//    public class Data {
//        public int GatewayIntegrationPk { get; set; }
//        public string? Klass { get; set; }
//        public DateTime CreatedAt { get; set; }
//        public double Amount { get; set; }
//        public string? Currency { get; set; }
//        public MigsOrder? MigsOrder { get; set; }
//        public string? Merchant { get; set; }
//        public string? MigsResult { get; set; }
//        public MigsTransaction? MigsTransaction { get; set; }
//        public string? TxnResponseCode { get; set; }
//        public string? AcqResponseCode { get; set; }
//        public string? Message { get; set; }
//        public string? MerchantTxnRef { get; set; }
//        public string? OrderInfo { get; set; }
//        public string? ReceiptNo { get; set; }
//        public string? TransactionNo { get; set; }
//        public int BatchNo { get; set; }
//        public string? AuthorizeId { get; set; }
//        public string? CardType { get; set; }
//        public string? CardNum { get; set; }
//        public string? SecureHash { get; set; }
//        public string? AvsResultCode { get; set; }
//        public string? AvsAcqResponseCode { get; set; }
//        public double CapturedAmount { get; set; }
//        public double AuthorisedAmount { get; set; }
//        public double RefundedAmount { get; set; }
//        public string? AcsEci { get; set; }
//    }

//    public class MigsOrder {
//        public bool AcceptPartialAmount { get; set; }
//        public double Amount { get; set; }
//        public string? AuthenticationStatus { get; set; }
//        public Chargeback? Chargeback { get; set; }
//        public DateTime CreationTime { get; set; }
//        public string? Currency { get; set; }
//        public string? Description { get; set; }
//        public string? Id { get; set; }
//        public DateTime LastUpdatedTime { get; set; }
//        public double MerchantAmount { get; set; }
//        public string? MerchantCategoryCode { get; set; }
//        public string? MerchantCurrency { get; set; }
//        public string? Status { get; set; }
//        public double TotalAuthorizedAmount { get; set; }
//        public double TotalCapturedAmount { get; set; }
//        public double TotalRefundedAmount { get; set; }
//    }

//    public class Chargeback {
//        public int Amount { get; set; }
//        public string? Currency { get; set; }
//    }

//    public class MigsTransaction {
//        public Acquirer? Acquirer { get; set; }
//        public double Amount { get; set; }
//        public string? AuthenticationStatus { get; set; }
//        public string? AuthorizationCode { get; set; }
//        public string? Currency { get; set; }
//        public string? Id { get; set; }
//        public string? Receipt { get; set; }
//        public string? Source { get; set; }
//        public string? Stan { get; set; }
//        public string? Terminal { get; set; }
//        public string? Type { get; set; }
//    }

//    public class Acquirer {
//        public int Batch { get; set; }
//        public string? Date { get; set; }
//        public string? Id { get; set; }
//        public string? MerchantId { get; set; }
//        public DateTime SettlementDate { get; set; }
//        public string? TimeZone { get; set; }
//        public string? TransactionId { get; set; }
//    }

//    public class PaymentKeyClaims {
//        public int Exp { get; set; }
//        public Dictionary<string, string?>? Extra { get; set; }
//        public string? PmkIp { get; set; }
//        public int UserId { get; set; }
//        public string? Currency { get; set; }
//        public int OrderId { get; set; }
//        public int AmountCents { get; set; }
//        public BillingData? BillingData { get; set; }
//        public int IntegrationId { get; set; }
//        public bool LockOrderWhenPaid { get; set; }
//        public bool SinglePaymentAttempt { get; set; }
//    }

//    public class BillingData {
//        public string? City { get; set; }
//        public string? Email { get; set; }
//        public string? Floor { get; set; }
//        public string? State { get; set; }
//        public string? Street { get; set; }
//        public string? Country { get; set; }
//        public string? Building { get; set; }
//        public string? Apartment { get; set; }
//        public string? LastName { get; set; }
//        public string? FirstName { get; set; }
//        public string? PostalCode { get; set; }
//        public string? PhoneNumber { get; set; }
//        public string? ExtraDescription { get; set; }
//    }


//}
