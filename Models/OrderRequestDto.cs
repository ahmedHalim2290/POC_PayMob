using Newtonsoft.Json;

namespace POC_PayMob.Models {


    public class OrderItem {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }

    public class BillingData {
        [JsonProperty("apartment")]
        public string Apartment { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("floor")]
        public string Floor { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class Customer {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("extras")]
        public object Extras { get; set; }
    }

    public class OrderRequestDto {

        [JsonProperty("payment_methods")]
        public object[] PaymentMethods { get; set; }
     //   [JsonProperty("special_reference")]
      //  public string SpecialReference {  get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("items")]
        public List<OrderItem> Items { get; set; }

        [JsonProperty("billing_data")]
        public BillingData BillingData { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("extras")]
        public object Extras { get; set; }
    }
}