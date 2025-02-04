// Ignore Spelling: Paymob

using System.Security.Policy;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Common;
using POC_PayMob.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace POC_PayMob.Services {
    public class PaymobService {

       
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PaymobOptions _options;




        public PaymobService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,IOptions<PaymobOptions> options)
        {
            _options = options.Value;
        
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<string> GetClientSecretAsync(OrderRequestDto payload, bool IsAuth)
        {
            var IntegrationID = IsAuth?_options.AuthIntegrationID:_options.StandAloneIntegrationID;

            payload.PaymentMethods = new object[] { "card", IntegrationID };
            var clientSecretResponse = await PostAsync(payload, _options.PaymentTransactionURL);
            var result = JsonConvert.DeserializeObject<dynamic>(clientSecretResponse);

            return result.client_secret;
        }

        public async Task<dynamic> CaptureTransactionAsync(ProcessRequestDto captureDto)
        {
            return await PostAsync(captureDto, _options.CaptureTransactionURL);
        }
        public async Task<dynamic> VoidTransactionAsync(VoidRequestDto voidDto)
        {
            return await PostAsync(voidDto, _options.VoidTransactionURL);
        }
        public async Task<dynamic> RefundTransactionAsync(ProcessRequestDto refundDto)
        {
            return await PostAsync(refundDto, _options.RefundTransactionURL);
        }
      
        private async Task<string> GetAuthTokenAsync()
        {
            var result= await PostWithTokenAsync(new { api_key = _options.APIKey }, _options.AuthTokenURL);
            var token = JsonConvert.DeserializeObject<dynamic>(result).token;
            return token;
        }
        public async Task<dynamic> GetTransactionByMerchantOrderIdAsync(string MerchantOrderId)
        {
            var token = await GetAuthTokenAsync();
            return await PostWithTokenAsync(new { auth_token =token,merchant_order_id= MerchantOrderId },_options.TransacrionByMerchantOrderIdURL);
        }
        public async Task<dynamic> GetTransactionByOrderIdAsync(int orderId)
        {
            var token = await GetAuthTokenAsync();
            return await PostWithTokenAsync(new { auth_token = token, order_id= orderId }, _options.TransacrionByOrderIdURL);
        }
        public async Task<dynamic> GetTransactionByTrxIdAsync(int transactionID)
        {
            var token = await GetAuthTokenAsync();
            return await GetWithTokenAsync(new { auth_token = token },_options.TransacrionByTrxIdURL+"/"+transactionID );
        }
        public async Task<string> PostWithTokenAsync(object data, string url)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
           
        
        }
        public async Task<string> GetWithTokenAsync(object data, string url)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
           
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
           

        }
        public async Task<string> PostAsync(object data, string url)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", "Token " + _options.authToken);
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
           /* //For Testing
           if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {responseContent}");
            }*/
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
