using Microsoft.AspNetCore.Mvc;
using POC_PayMob.Models;
using POC_PayMob.Services;
using System.Diagnostics;

namespace POC_PayMob.Controllers {
    public class HomeController : Controller {
        private readonly PaymobService _paymobService;

        public HomeController(PaymobService paymobService)
        {
            _paymobService = paymobService;
        }

        public async Task<IActionResult> Index()
        {
            var paymentToken = await _paymobService.GetPaymentTokenAsync(10, "EGP", 232523234);
            var model = new { PaymentToken = paymentToken };
            return View(model);

        }
    }
}
