namespace POC_PayMob.Binders {
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using POC_PayMob.Models;
    using System.Threading.Tasks;

    public class CallBackTransactionModelBinder : IModelBinder {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.ActionContext.HttpContext.Request;

            var result = new CallBackTransaction
            {
                Id = request.Query["id"],
                Pending = request.Query["pending"],
                AmountCents = request.Query["amount_cents"],
                Success = request.Query["success"],
                IsAuth = request.Query["is_auth"],
                IsCapture = request.Query["is_capture"],
                IsStandalonePayment = request.Query["is_standalone_payment"],
                IsVoided = request.Query["is_voided"],
                IsRefunded = request.Query["is_refunded"],
                Is3DSecure = request.Query["is_3d_secure"],
                IntegrationId = request.Query["integration_id"],
                ProfileId = request.Query["profile_id"],
                HasParentTransaction = request.Query["has_parent_transaction"],
                Order = request.Query["order"],
                CreatedAt = DateTime.Parse(request.Query["created_at"]),
                Currency = request.Query["currency"],
                MerchantCommission = request.Query["merchant_commission"],
                DiscountDetails = request.Query["discount_details"],
                IsVoid = request.Query["is_void"],
                IsRefund = request.Query["is_refund"],
                ErrorOccured = request.Query["error_occured"],
                RefundedAmountCents = request.Query["refunded_amount_cents"],
                CapturedAmount = request.Query["captured_amount"],
                UpdatedAt = DateTime.Parse(request.Query["updated_at"]),
                IsSettled = request.Query["is_settled"],
                BillBalanced = request.Query["bill_balanced"],
                IsBill = request.Query["is_bill"],
                Owner = request.Query["owner"],
                DataMessage = request.Query["data.message"],
                SourceDataType = request.Query["source_data.type"],
                SourceDataPan = request.Query["source_data.pan"],
                SourceDataSubType = request.Query["source_data.sub_type"],
                AcqResponseCode = request.Query["acq_response_code"],
                TxnResponseCode = request.Query["txn_response_code"],
                Hmac = request.Query["hmac"]
            };

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
