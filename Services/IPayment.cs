using POC_PayMob.Models;

namespace POC_PayMob.Services {
    public interface IPayment {
        Task<dynamic> CaptureTransactionAsync(ProcessRequestDto captureDto);
        Task<string> GetClientSecretAsync(OrderRequestDto payload, bool IsAuth);
        Task<dynamic> GetTransactionByMerchantOrderIdAsync(string MerchantOrderId);
        Task<dynamic> GetTransactionByOrderIdAsync(int orderId);
        Task<dynamic> GetTransactionByTrxIdAsync(int transactionID);
        Task<string> GetWithTokenAsync(object data, string url);
        Task<string> PostAsync(object data, string url);
        Task<string> PostWithTokenAsync(object data, string url);
        Task<dynamic> RefundTransactionAsync(ProcessRequestDto refundDto);
        Task<dynamic> VoidTransactionAsync(VoidRequestDto voidDto);
    }
}
