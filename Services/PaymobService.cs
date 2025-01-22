// Ignore Spelling: Paymob

using System.Text;
using Newtonsoft.Json;


namespace POC_PayMob.Services {
    public class PaymobService {
        private readonly HttpClient _httpClient;
        //from pay mob setting
        private readonly string _apiKey = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2ljSEp2Wm1sc1pWOXdheUk2TVRBeE9Ea3dNU3dpYm1GdFpTSTZJakUzTXpjek1qQTNNVEV1TlRrek9EQTFJbjAuOGIxbk9XckYzLVNMOGJxS0cwWFYxSldzUnE5ME1ZTmpGaDdWcWxFeUFGR0kyRWxtTmVFLUtyVjVVc2ZVVUVlUkNGbUpNd05MZmx0UlJYb2FaM0dhV1E=";

        public PaymobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
          
        }

        public async Task<string> GetPaymentTokenAsync(decimal amount, string currency, int orderId)
        {
            var authToken = await GetAuthTokenAsync();

            var order = new
            {

                auth_token = authToken,
                amount_cents = amount * 100, // Paymob expects amount in cents
                currency,
                order_id = orderId,
                delivery_needed = "false",
                items = new object[] { }
            };

            var orderResponse = await PostAsync("https://accept.paymobsolutions.com/api/ecommerce/orders", order);
            var orderIdResponse = JsonConvert.DeserializeObject<dynamic>(orderResponse).id;

            var paymentTokenRequest = new
            {
                auth_token = authToken,
                integration_id = 4930839 ,
                order_id = orderIdResponse,
                currency,

                amount_cents = amount * 100,
                expiration = 3600,
                billing_data = new
                {
                    
                    first_name = "John",
                    last_name = "Doe",
                    email = "john.doe@example.com",
                    phone_number = "+201010101010",
                    country = "EG",
                    city = "Cairo",
                    street = "123 Street",
                    building = "123",
                    floor = "4",
                    apartment = "8"
                },
                extra_data = new            //this to adding an extra data to the payment 
                {
                    custom_field_1 = "value1",
                    custom_field_2 = "value2"
                }
            };

            var paymentTokenResponse = await PostAsync("https://accept.paymobsolutions.com/api/acceptance/payment_keys", paymentTokenRequest);
            return JsonConvert.DeserializeObject<dynamic>(paymentTokenResponse).token;
        }

        private async Task<string> GetAuthTokenAsync()
        {
            var authRequest = new
            {
                api_key = _apiKey
            };

            var response = await PostAsync("https://accept.paymobsolutions.com/api/auth/tokens", authRequest);
            return JsonConvert.DeserializeObject<dynamic>(response).token;
        }

        private async Task<string> PostAsync(string url, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
