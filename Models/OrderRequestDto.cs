namespace POC_PayMob.Models {
    
    public class OrderItem
    {
        public string name { get; set; }
        public decimal amount { get; set; }
         public string description { get; set; }
          public decimal quantity { get; set; }
    }
    public class OrderBillingData
    {
       public string apartment { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string street { get; set; }
        public string building { get; set; }
        public string phone_number { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string floor { get; set; }
        public string  state { get; set; }
    }
    public class OrderCustomer
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public object extras { get; set; }
    }
    public class OrderRequestDto {
        private static readonly int _AuthPaymentMethodIntegrationID = 4937477;
        private static readonly int _AuthPaymentMethodNum = 12;
        private static readonly string _authPaymentMethodType = "card";
        public object[] payment_methods = new object[] 
        { _AuthPaymentMethodNum, _authPaymentMethodType, _AuthPaymentMethodIntegrationID };

          public decimal amount { get; set; }
          public string  currency { get; set; }
          public List<OrderItem> items { get; set; }   
          public OrderBillingData  billing_data { get; set; }
          public OrderCustomer customer { get; set; }
          public object extras { get; set; }

    }
}
