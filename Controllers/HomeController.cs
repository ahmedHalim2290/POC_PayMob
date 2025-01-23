using Microsoft.AspNetCore.Mvc;
using POC_PayMob.Filters;
using POC_PayMob.Models;
using POC_PayMob.Services;
using System.Diagnostics;
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
            var _hmac = hmac;
            var paymentToken = await _paymobService.GetPaymentTokenAsync(10, "EGP", 232523234);
            var model = new { PaymentToken = paymentToken };
            return View(model);

        }
        [ServiceFilter(typeof(HmacValidationFilter))] // Apply the HMAC filter
        public async Task<IActionResult> Thank()
        {

            return View();

        }
    }
}

