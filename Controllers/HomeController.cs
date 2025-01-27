using Microsoft.AspNetCore.Mvc;
using POC_PayMob.Binders;
using POC_PayMob.Filters;
using POC_PayMob.Models;
using POC_PayMob.Services;
using System.Diagnostics;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace POC_PayMob.Controllers {
    public class HomeController : Controller {
        private readonly PaymobService _paymobService;

        public HomeController(PaymobService paymobService)
        {
            _paymobService = paymobService;
        }


        public async Task<IActionResult> Index([FromQuery] string hmac)
        {
          /*  var _hmac = hmac;
            var paymentToken = await _paymobService.GetPaymentTokenAsync(10, "EGP", "232523234");
            var model = new { PaymentToken = paymentToken };*/
            var clientSecret = await _paymobService.GetClientSecretAsync();
            var model = new { clientSecret = clientSecret };
            return View(model);

        }
      // [ServiceFilter(typeof(HmacValidationFilter))] // Apply the HMAC filter
        /*public async Task<IActionResult> Thank([ModelBinder(BinderType = typeof(CallBackTransactionModelBinder)CallBackTransaction response)*/
        public async Task<IActionResult> Thank()
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

