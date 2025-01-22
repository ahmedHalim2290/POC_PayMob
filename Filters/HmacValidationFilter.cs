using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
namespace POC_PayMob.Filters {

    public class HmacValidationFilter : IAsyncActionFilter {
        private const string PayMobSecretKey = "egy_sk_test_331276c1f8cee7d6cdb8f2f9412a8d53c3967ed6091a79d8d4b2cd865c2870cc"; // Replace with your actual secret key

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Step 1: Read the request body
            var requestBody = await ReadRequestBodyAsync(context.HttpContext.Request);

            // Step 2: Parse the request body to extract the HMAC
            var receivedHmac = context.HttpContext.Request.Query["hmac"].ToString();

            if (string.IsNullOrEmpty(receivedHmac))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            // Step 3: Compute the HMAC using the secret key and request body
            var computedHmac = ComputeHmac(PayMobSecretKey, requestBody);

            // Step 4: Compare the computed HMAC with the received HMAC
            if (!string.Equals(computedHmac, receivedHmac, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            // HMAC validation successful, proceed to the action
            await next();
        }

        private async Task<string> ReadRequestBodyAsync(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            request.EnableBuffering(); // Enable rewinding the request body stream
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0; // Reset the stream position for further processing
                return body;
            }
        }

        private string ExtractHmacFromRequestBody(string requestBody)
        {
            try
            {
                // Parse the JSON request body
                var json = JObject.Parse(requestBody);

                // Extract the HMAC from the query object inside the event object
                var hmac = json["event"]?["query"]?["hmac"]?.ToString();
                return hmac;
            }
            catch
            {
                return null;
            }
        }

        private string ComputeHmac(string secret, string message)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
