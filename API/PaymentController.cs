using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_PayMob.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using POC_PayMob.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using POC_PayMob.Filters;
namespace POC_PayMob.API {

    [ApiController]
    [Route("api/[controller]")]

    public class PaymentController : ControllerBase {
        private readonly PaymobService _paymobService;

        public PaymentController(PaymobService paymobService)
        {
            _paymobService = paymobService;
        }


        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
        {
            var paymentToken = await _paymobService.GetPaymentTokenAsync(request.Amount, request.Currency, request.OrderId);
            return Ok(new { PaymentToken = paymentToken });
        }

        [HttpPost("payment-callback")]
        [ServiceFilter(typeof(HmacValidationFilter))] // Apply the HMAC filter
        public IActionResult PaymentCallback([FromBody] object response)
        {
            // Handle the payment response
            object? newResult = JsonConvert.DeserializeObject(response.ToString());

            PayMobResponseDto createdOrder = JsonConvert.DeserializeObject<PayMobResponseDto>(response.ToString());


            return Ok();
        }

        [HttpGet("GetName")]
        public string GetName()
        {
            return "Hi From NgRok";
        }

        [HttpGet("GetToken")]
        public async Task<string> GetAuthToken()
        {
            return await _paymobService.GetAuthTokenAsync();
        }
        [HttpPost("GetOrderById")]
        public async Task<Order> GetOrderByIdAsync([FromQuery] string orderId)
        {
            return await _paymobService.GetOrderByIdAsync(orderId);
        }
    }
    public class PaymentCallbackResponse {
        public bool Success { get; set; }
        public string Message { get; set; }
        // Other fields as per Paymob's documentation
    }

    public class PaymentRequest {
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public int OrderId { get; set; }
    }
}
