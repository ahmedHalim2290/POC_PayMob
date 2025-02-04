using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC_PayMob.Models;
using POC_PayMob.Services;

namespace POC_PayMob.Controllers {
    public class HomeController : Controller {
        private readonly PaymobService _paymobService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(PaymobService paymobService, IHttpContextAccessor httpContextAccessor)
        {
            _paymobService = paymobService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> Index()
        {
            OrderRequestDto order = new OrderRequestDto()  // request model i should to create the transaction Auth OR standalone
            {
            //    SpecialReference = "orderId_7676",
                Amount = 2000,
                Currency = "EGP",
                Items = new List<OrderItem>(){
                    new OrderItem(){
                        Name= "Item name",
                        Amount= 2000,
                        Description= "Item description",
                         Quantity= 1
                    }
                },
                BillingData = new BillingData
                {
                    Apartment = "dumy",
                    FirstName = "ala",
                    LastName = "zain",
                    Street = "dumy",
                    Building = "dumy",
                    PhoneNumber = "+92345xxxxxxxx",
                    City = "dumy",
                    Country = "dumy",
                    Email = "ali@gmail.com",
                    Floor = "dumy",
                    State = "dumy"
                },
                Extras = new
                {
                    ee = 22
                }
            };


            var clientSecret = await _paymobService.GetClientSecretAsync(order,true);
            var model = new { clientSecret = clientSecret };
            return View(model);

        }

        public async Task<IActionResult> ConfirmOrder([FromQuery] ProcessRequestDto captureDTo)
        {
             var data = await _paymobService.CaptureTransactionAsync(captureDTo);
            TransactionResponseDto model = new TransactionResponseDto();
            model = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return View(model);
        }
        public async Task<IActionResult> CancelOrder([FromQuery] VoidRequestDto voidDto)
        {
            var data = await _paymobService.VoidTransactionAsync(voidDto);
            TransactionResponseDto model = new TransactionResponseDto();
            model = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return View(model);
        }

        public async Task<IActionResult> RefundOrder([FromQuery] ProcessRequestDto refundDto)
        {
            var data = await _paymobService.RefundTransactionAsync(refundDto);
            TransactionResponseDto model = new TransactionResponseDto();
            model = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return View(model);
        }
        public async Task<IActionResult> GetTransactionById(int TransactionId)
        {
            var data = await _paymobService.GetTransactionByTrxIdAsync(TransactionId);
            TransactionResponseDto model = new TransactionResponseDto();
            model = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return View("Transaction", model);
        }
        public async Task<IActionResult> TransactionByOrderId(int orderId)
        {
            var data = await _paymobService.GetTransactionByOrderIdAsync(orderId);
            TransactionResponseDto model = new TransactionResponseDto();
            model = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return View("Transaction", model);
        }
        public async Task<IActionResult> TransactionByMerchantOrderId(string MerchantOrderId)
        {
            var data = await _paymobService.GetTransactionByMerchantOrderIdAsync(MerchantOrderId);
            TransactionResponseDto model = new TransactionResponseDto();
            model = JsonConvert.DeserializeObject<TransactionResponseDto>(data.ToString());
            return View("Transaction", model);
        }


        public async Task<IActionResult> RedirectOrder()
        {
            var request = HttpContext.Request;

            var result = new RedirectionPaymentResponseDto
            {
                Id = request.Query["id"].ToString(),
                Pending = request.Query["pending"].ToString(),
                AmountCents = request.Query["amount_cents"].ToString(),
                Success = request.Query["success"].ToString(),
                IsAuth = request.Query["is_auth"].ToString(),
                IsCapture = request.Query["is_capture"].ToString(),
                IsStandalonePayment = request.Query["is_standalone_payment"].ToString(),
                IsVoided = request.Query["is_voided"].ToString(),
                IsRefunded = request.Query["is_refunded"].ToString(),
                Is3DSecure = request.Query["is_3d_secure"].ToString(),
                IntegrationId = request.Query["integration_id"].ToString(),
                ProfileId = request.Query["profile_id"].ToString(),
                HasParentTransaction = request.Query["has_parent_transaction"].ToString(),
                Order = request.Query["order"].ToString(),
               CreatedAt = DateTime.Parse(request.Query["created_at"].ToString()),
                Currency = request.Query["currency"].ToString(),
                MerchantCommission = request.Query["merchant_commission"].ToString(),
                DiscountDetails = request.Query["discount_details"].ToString(),
                IsVoid = request.Query["is_void"].ToString(),
                IsRefund = request.Query["is_refund"].ToString(),
                ErrorOccured = request.Query["error_occured"].ToString(),
                RefundedAmountCents = request.Query["refunded_amount_cents"].ToString(),
                CapturedAmount = request.Query["captured_amount"].ToString(),
                UpdatedAt = DateTime.Parse(request.Query["updated_at"].ToString()),
                IsSettled = request.Query["is_settled"].ToString(),
                BillBalanced = request.Query["bill_balanced"].ToString(),
                IsBill = request.Query["is_bill"].ToString(),
                Owner = request.Query["owner"].ToString(),
                DataMessage = request.Query["data.message"].ToString(),
                SourceDataType = request.Query["source_data.type"].ToString(),
                SourceDataPan = request.Query["source_data.pan"].ToString(),
                SourceDataSubType = request.Query["source_data.sub_type"].ToString(),
                AcqResponseCode = request.Query["acq_response_code"].ToString(),
                TxnResponseCode = request.Query["txn_response_code"].ToString(),
                Hmac = request.Query["hmac"].ToString()
            };

            return View(result);

        }
    }
}

