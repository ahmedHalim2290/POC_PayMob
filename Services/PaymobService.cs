// Ignore Spelling: Paymob

using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POC_PayMob.Models;


namespace POC_PayMob.Services {
    public class PaymobService {

       
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PaymobOptions _options;

        private string authToken;
        private string PaymentTransactionURL;
        private string CaptureTransactionURL;
        private string VoidTransactionURL;


        public PaymobService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,IOptions<PaymobOptions> options)
        {
            _options = options.Value;
        
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            authToken = _options.authToken;
            PaymentTransactionURL = _options.PaymentTransactionURL;
            CaptureTransactionURL = _options.CaptureTransactionURL;
            VoidTransactionURL = _options.VoidTransactionURL;
        }

        public async Task<string> GetClientSecretAsync(OrderRequestDto payload)
        {
            var clientSecretResponse = await PostAsync(payload, PaymentTransactionURL);
            var result = JsonConvert.DeserializeObject<dynamic>(clientSecretResponse);

            return result.client_secret;
        }

        public async Task<dynamic> CaptureTransactionAsync(CaptureRequestDto captureDto)
        {
            return await PostAsync(captureDto, CaptureTransactionURL);
        }
        public async Task<dynamic> VoidTransactionAsync(VoidRequestDto voidDto)
        {
            return await PostAsync(voidDto, VoidTransactionURL);
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
