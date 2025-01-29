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
                amount = 2000,
                currency = "EGP",
                items = new List<OrderItem>(){
                    new OrderItem(){
                        name= "Item name",
                        amount= 2000,
                        description= "Item description",
                         quantity= 1
                    }
                },
                billing_data = new OrderBillingData
                {
                    apartment = "dumy",
                    first_name = "ala",
                    last_name = "zain",
                    street = "dumy",
                    building = "dumy",
                    phone_number = "+92345xxxxxxxx",
                    city = "dumy",
                    country = "dumy",
                    email = "ali@gmail.com",
                    floor = "dumy",
                    state = "dumy"
                },
                extras = new
                {
                    ee = 22
                }
            };


            var clientSecret = await _paymobService.GetClientSecretAsync(order);
            var model = new { clientSecret = clientSecret };
            return View(model);

        }

        public async Task<IActionResult> CaptureOrder([FromQuery] CaptureRequestDto captureDTo)
        {
            var data = await _paymobService.CaptureTransactionAsync(captureDTo);
            Response_Transaction model = new Response_Transaction();
            model = JsonConvert.DeserializeObject<Response_Transaction>(data);
            return View(model);
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var request = HttpContext.Request;
            var result = new CallBackTransaction
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
                //    CreatedAt = DateTime.Parse(request.Query["created_at"].ToString()),
                Currency = request.Query["currency"].ToString(),
                MerchantCommission = request.Query["merchant_commission"].ToString(),
                DiscountDetails = request.Query["discount_details"].ToString(),
                IsVoid = request.Query["is_void"].ToString(),
                IsRefund = request.Query["is_refund"].ToString(),
                ErrorOccured = request.Query["error_occured"].ToString(),
                RefundedAmountCents = request.Query["refunded_amount_cents"].ToString(),
                CapturedAmount = request.Query["captured_amount"].ToString(),
                //      UpdatedAt = DateTime.Parse(request.Query["updated_at"].ToString()),
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

