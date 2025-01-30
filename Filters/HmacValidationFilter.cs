using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using POC_PayMob.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace POC_PayMob.Filters {

    public class HmacValidationFilter : IAsyncActionFilter {
       
        private readonly PaymobOptions _options;
        private string HMACSecret;
       public HmacValidationFilter(IOptions<PaymobOptions> options) 
        {
            _options = options.Value;
           
            HMACSecret = _options.HMACSecret;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
                var currentRequest = context.HttpContext.Request;
                // Step 1: Read the request body
                var requestBody = await ReadRequestBodyAsync(context.HttpContext.Request);

                // Step 2: Parse the request body to extract the HMAC
                var receivedHmac = currentRequest.Query["hmac"].ToString();
                if (string.IsNullOrEmpty(receivedHmac))
                {
                    throw new Exception("HMAC not found in the request body.");
                }
                // Step 3: Compute the HMAC using the secret key and request body
                var computedHmac = ComputeHmac(HMACSecret, requestBody);

                // Step 4: Compare the computed HMAC with the received HMAC
                if (!string.Equals(computedHmac, receivedHmac, StringComparison.OrdinalIgnoreCase))
                {
                       context.Result = new UnauthorizedResult();
                       return;
                }
                // HMAC validation successful, proceed to the action
                await next();
            
            
        }

        private string ComputeHmac(string secret, string requestBody)
        {
                // Step 1: Deserialize the request body
                PaymentResponseDto payMobData = JsonConvert.DeserializeObject<PaymentResponseDto>(requestBody);

                // Step 2: Prepare the data for HMAC calculation
                var data = new Dictionary<string, string>
        {
            { "amount_cents", payMobData.Obj.AmountCents.ToString() },
            { "created_at", payMobData.Obj.CreatedAt},
            { "currency", payMobData.Obj.Currency ?? string.Empty },
            { "error_occured", payMobData.Obj.ErrorOccured.ToString().ToLower() },
            { "has_parent_transaction", payMobData.Obj.HasParentTransaction.ToString().ToLower()},
            { "id", payMobData.Obj.Id.ToString() },
            { "integration_id", payMobData.Obj.IntegrationId.ToString() },
            { "is_3d_secure", payMobData.Obj.Is3DSecure.ToString().ToLower() },
            { "is_auth", payMobData.Obj.IsAuth.ToString().ToLower()},
            { "is_capture", payMobData.Obj.IsCapture.ToString().ToLower() },
            { "is_refunded", payMobData.Obj.IsRefunded.ToString().ToLower()},
            { "is_standalone_payment", payMobData.Obj.IsStandalonePayment.ToString().ToLower() },
            { "is_voided", payMobData.Obj.IsVoided.ToString().ToLower() },
            { "order.id", payMobData.Obj.Order?.Id.ToString() ?? string.Empty },
            { "owner", payMobData.Obj.Owner.ToString() },
            { "pending", payMobData.Obj.Pending.ToString().ToLower()},
            { "source_data.pan", payMobData.Obj.SourceData?.Pan?? string.Empty },
            { "source_data.sub_type", payMobData.Obj.SourceData?.SubType?? string.Empty },
            { "source_data.type", payMobData.Obj.SourceData?.Type?? string.Empty },
            { "success", payMobData.Obj.Success.ToString().ToLower()}
        };

                // Step 3: Sort the keys lexicographically
                var sortedKeys = data.Keys.OrderBy(k => k, StringComparer.Ordinal).ToList();

                // Step 4: Concatenate the values in the specified order
                var sb = new StringBuilder();
                foreach (var key in sortedKeys)
                {
                    sb.Append(data[key]);
                }

                // Step 5: Compute the HMAC-SHA512 hash
                using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secret)))
                {
                    byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
           
        }
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
           
            request.EnableBuffering(); // Enable rewinding the request body stream
            request.Body.Position = 0;
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
               var  requestBody = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return requestBody;
            }
        }
       
    
}

}
