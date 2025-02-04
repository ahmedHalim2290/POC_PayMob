using Microsoft.AspNetCore.Mvc;
using POC_PayMob.Services;
using POC_PayMob.Models;
using Newtonsoft.Json;
using POC_PayMob.Filters;

namespace POC_PayMob.API {
    /// <summary>
    /// Controller responsible for handling payment-related operations such as transactions, callbacks, and refunds.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase {
        private readonly PaymobService _paymobService;

        /// <summary>
        /// Initializes a new instance of the <seecref="PaymentController"/> class.
        /// </summary>
        /// <paramname="paymobService">The service used for interacting with Paymob's API.</param>
        public PaymentController(PaymobService paymobService)
        {
            _paymobService = paymobService;
        }

        /// <summary>
        /// Retrieves the client secret key for a payment transaction.
        /// </summary>
        /// <paramname="order">The order details required to generate the client secret.</param>
        /// <paramname="IsAuth">Indicates whether the transaction requires authentication.</param>
        /// <returns>The client secret key for the payment transaction.</returns>
        [HttpPost("payment-transaction")]
        public async Task<IActionResult> GetClientSecret([FromBody] OrderRequestDto order, bool IsAuth)
        {
            var clientSecret = await _paymobService.GetClientSecretAsync(order, IsAuth);
            return Ok(new { client_secret_key = clientSecret });
        }

        /// <summary>
        /// Handles the callback from Paymob after a payment is processed.
        /// </summary>
        /// <paramname="response">The payment response object from Paymob.</param>
        /// <returns>The processed payment response.</returns>
        [HttpPost("payment-callback")]
        [ServiceFilter(typeof(HmacValidationFilter))] // Apply the HMAC filter
        public IActionResult PaymentCallback([FromBody] object response)
        {
            PaymentResponseDto createdOrder = JsonConvert.DeserializeObject<PaymentResponseDto>(response.ToString());
            return Ok(createdOrder.Obj);
        }

        /// <summary>
        /// Captures a previously authorized payment transaction.
        /// </summary>
        /// <paramname="captureDto">The details required to capture the transaction.</param>
        /// <returns>The response from the capture transaction request.</returns>
        [HttpPost("capture-transaction")]
        public async Task<IActionResult> CaptureTransaction([FromBody] ProcessRequestDto captureDto)
        {
            var data = await _paymobService.CaptureTransactionAsync(captureDto);
            TransactionResponseDto response = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return Ok(response);
        }

        /// <summary>
        /// Cancels a previously authorized payment transaction.
        /// </summary>
        /// <paramname="voidDto">The details required to void the transaction.</param>
        /// <returns>The response from the void transaction request.</returns>
        [HttpPost("cancel-transaction")]
        public async Task<IActionResult> CancelOrder([FromBody] VoidRequestDto voidDto)
        {
            var data = await _paymobService.VoidTransactionAsync(voidDto);
            TransactionResponseDto response = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return Ok(response);
        }

        /// <summary>
        /// Refunds a previously captured payment transaction.
        /// </summary>
        /// <paramname="refundDto">The details required to process the refund.</param>
        /// <returns>The response from the refund transaction request.</returns>
        [HttpPost("refund-transaction")]
        public async Task<IActionResult> RefundOrder([FromBody] ProcessRequestDto refundDto)
        {
            var data = await _paymobService.RefundTransactionAsync(refundDto);
            TransactionResponseDto response = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a transaction by its unique transaction ID.
        /// </summary>
        /// <paramname="TransactionId">The unique identifier of the transaction.</param>
        /// <returns>The transaction details.</returns>
        [HttpGet("transaction-by-trxId")]
        public async Task<IActionResult> GetTransactionById(int TransactionId)
        {
            var data = await _paymobService.GetTransactionByTrxIdAsync(TransactionId);
            TransactionResponseDto response = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a transaction by its associated order ID.
        /// </summary>
        /// <paramname="orderId">The unique identifier of the order associated with the transaction.</param>
        /// <returns>The transaction details.</returns>
        [HttpGet("transaction-by-orderId")]
        public async Task<IActionResult> TransactionByOrderId(int orderId)
        {
            var data = await _paymobService.GetTransactionByOrderIdAsync(orderId);
            TransactionResponseDto response = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a transaction by its associated merchant order ID.
        /// </summary>
        /// <paramname="MerchantOrderId">The unique identifier of the merchant order associated with the transaction.</param>
        /// <returns>The transaction details.</returns>
        [HttpGet("transaction-by-MerchantOrderId")]
        public async Task<IActionResult> TransactionByMerchantOrderId(string MerchantOrderId)
        {
            var data = await _paymobService.GetTransactionByMerchantOrderIdAsync(MerchantOrderId);
            TransactionResponseDto response = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return Ok(response);
        }
    }
}