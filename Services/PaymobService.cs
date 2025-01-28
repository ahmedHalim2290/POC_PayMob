// Ignore Spelling: Paymob

using System.Text;
using Newtonsoft.Json;
using POC_PayMob.Models;


namespace POC_PayMob.Services {
    public class PaymobService {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //from pay mob setting
        private readonly string _apiKey = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2ljSEp2Wm1sc1pWOXdheUk2TVRBeE9Ea3dNU3dpYm1GdFpTSTZJakUzTXpjek1qQTNNVEV1TlRrek9EQTFJbjAuOGIxbk9XckYzLVNMOGJxS0cwWFYxSldzUnE5ME1ZTmpGaDdWcWxFeUFGR0kyRWxtTmVFLUtyVjVVc2ZVVUVlUkNGbUpNd05MZmx0UlJYb2FaM0dhV1E=";

        public PaymobService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GetClientSecretAsync()
        {

           var payload = new
            {
               auth=true,
               amount = 10,
                currency = "EGP",
                payment_methods = new object[] { 12, "card", 4930839 },
                items = new[]
        {
        new
        {
            name = "Item name 1",
            amount = 10,
            description = "Watch",
            quantity = 1
        }
    },
                billing_data = new
                {
                    apartment = "6",
                    first_name = "Ammar",
                    last_name = "Sadek",
                    street = "938, Al-Jadeed Bldg",
                    building = "939",
                    phone_number = "+96824480228",
                    country = "OMN",
                    email = "AmmarSadek@gmail.com",
                    floor = "1",
                    state = "Alkhuwair"
                },
                customer = new
                {
                    first_name = "Ammar",
                    last_name = "Sadek",
                    email = "AmmarSadek@gmail.com",
                    extras = new
                    {
                        re = "22"
                    }
                },
                extras = new
                {
                    ee = 22
                }
            };


            var paymentTokenResponse = await PostSecretAsync("https://accept.paymob.com/v1/intention/", payload);
            var result = JsonConvert.DeserializeObject<dynamic>(paymentTokenResponse);
            var extraData= result.extras;
            var session = _httpContextAccessor.HttpContext.Session;

            // Serialize the object to JSON
            string jsonObject = JsonConvert.SerializeObject(extraData);

            // Store the serialized object in session
            session.SetString("extraData", jsonObject);
            return result.client_secret;
        }

        public async Task<string> GetPaymentTokenAsync(decimal amount, string _currency, int orderId )
        {
            var authToken = await GetAuthTokenAsync();

            var order = new
            {

                auth_token = authToken,
                amount_cents = amount * 100, // Paymob expects amount in cents
                currency=_currency,
             //   merchant_order_id = orderId,
                delivery_needed = "false",
                items = new object[] { }
            };

            var orderResponse = await PostAsync("https://accept.paymobsolutions.com/api/ecommerce/orders", order);
            var orderIdResponse = JsonConvert.DeserializeObject<dynamic>(orderResponse).id;

            var paymentTokenRequest = new
            {
                auth_token = authToken,
                integration_id = 4930839,
                transaction_type= "auth",
                order_id = orderIdResponse,
                currency= _currency,

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
                extra = new            //this to adding an extra data to the payment 
                {
                    custom_field_1 = "value1",
                    custom_field_2 = "value2"
                }
            };

            var paymentTokenResponse = await PostAsync("https://accept.paymobsolutions.com/api/acceptance/payment_keys", paymentTokenRequest);

            return JsonConvert.DeserializeObject<dynamic>(paymentTokenResponse).token;
        }

        public async Task<string> GetAuthTokenAsync()
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
        private async Task<string> PostSecretAsync(string url, object data)
        {
           

            var authToken = "egy_sk_test_549543704060b5d0c3ebae6c98aabbacf464494c2b7d0452a01a36da89dfc4e6";
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accept.paymob.com/v1/intention/");
            request.Headers.Add("Authorization", "Token "+authToken);
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {responseContent}");
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        private  async Task<dynamic> CaptureTransaction(string paymentKey)
        {
            var content = new
            {
                source = new { identifier = "AGGREGATOR", subtype = "AGGREGATOR" },
                payment_token = paymentKey
            };


            var orderResponse = await PostAsync("https://accept.paymobsolutions.com/api/acceptance/capture", content);
            var x = JsonConvert.DeserializeObject<dynamic>(orderResponse);
            return x;
        }




        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            var authToken = await GetAuthTokenAsync();

            var order = new
            {

                auth_token = authToken,
                order_id = orderId
            };

            var orderResponse = await PostAsync("https://accept.paymob.com/api/ecommerce/orders/transaction_inquiry", order);
            return JsonConvert.DeserializeObject<Order>(orderResponse);
        }
    }

}
