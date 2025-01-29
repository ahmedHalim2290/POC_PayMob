// Ignore Spelling: Paymob

using System.Text;
using Newtonsoft.Json;
using POC_PayMob.Models;


namespace POC_PayMob.Services {
    public class PaymobService {

        private readonly string authToken = "egy_sk_test_549543704060b5d0c3ebae6c98aabbacf464494c2b7d0452a01a36da89dfc4e6";// secret key from dashboard
        private readonly string PaymentTransactionURL = "https://accept.paymob.com/v1/intention/"; // Auth or Standalone based on integration ID inside order class
        private readonly string CaptureTransactionURL = " https://accept.paymob.com/api/acceptance/capture";

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static dynamic? extraData;

        public PaymobService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetClientSecretAsync(OrderRequestDto payload)
        {
            var clientSecretResponse = await PostAsync(payload, PaymentTransactionURL);
            var result = JsonConvert.DeserializeObject<dynamic>(clientSecretResponse);

            return result.client_secret;
        }

        public async Task<dynamic> CaptureTransactionAsync(CaptureRequestDto captureDTo)
        {
            return await PostAsync(captureDTo, CaptureTransactionURL);
        }

        public async Task<string> PostAsync(object data, string url)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", "Token " + authToken);
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
    }

}
