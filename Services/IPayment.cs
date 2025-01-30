using POC_PayMob.Models;

namespace POC_PayMob.Services {
    public interface IPayment {
        Task<dynamic> CaptureTransactionAsync(CaptureRequestDto captureRequest);
        Task<string> GetClientSecretAsync(OrderRequestDto orderRequest);
        Task<string> PostAsync(object data, string url);
    }
}
