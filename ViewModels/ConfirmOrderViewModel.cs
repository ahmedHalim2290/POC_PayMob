using POC_PayMob.Models;

namespace POC_PayMob.ViewModels {
    public class ConfirmOrderViewModel {
        public RedirectionPaymentResponseDto RedirectionPaymentResponseDto { get; set; }
        public TransactionResponseDto TransactionResponseDto { get; set; }

    }
}
