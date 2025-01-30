using Microsoft.AspNetCore.Mvc;
using POC_PayMob.Services;
using POC_PayMob.Models;
using Newtonsoft.Json;
using POC_PayMob.Filters;
namespace POC_PayMob.API {

    [ApiController]
    [Route("api/[controller]")]
    // [EnableCors("AllowSpecificOrigin")] // Apply CORS policy to this controller
    public class PaymentController : ControllerBase {

        private readonly PaymobService _paymobService;

        public PaymentController(PaymobService paymobService)
        {
            _paymobService = paymobService;
        }

        [HttpPost("auth-transaction")]
        public async Task<IActionResult> GetClientSecret([FromBody] OrderRequestDto order)
        {
            var clientSecret = await _paymobService.GetClientSecretAsync(order);
            return Ok(new { client_secret_key = clientSecret });
        }

        [HttpPost("payment-callback")]
        [ServiceFilter(typeof(HmacValidationFilter))] // Apply the HMAC filter
        public IActionResult PaymentCallback([FromBody] object response)
        {
            // Handle the payment response
            PaymentResponseDto createdOrder = JsonConvert.DeserializeObject<PaymentResponseDto>(response.ToString());
            var extraData = createdOrder.Obj.PaymentKeyClaims.Extra;

            return StatusCode(200, new { Message = "PaymentAuthorizedSuccessfully" });
        }

        [HttpPost("capture-transaction")]
        public async Task<dynamic> CaptureTransaction([FromBody] CaptureRequestDto captureDto)
        {
            return await _paymobService.CaptureTransactionAsync(captureDto);
        }

    }
}
