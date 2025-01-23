using Microsoft.AspNetCore.Mvc.ModelBinding;
using POC_PayMob.Models;

namespace POC_PayMob.Binders {
    public class CallBackTransactionModelBinderProvider : IModelBinderProvider {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Check if the model type is CallBackTransaction
            if (context.Metadata.ModelType == typeof(CallBackTransaction))
            {
                return new CallBackTransactionModelBinder();
            }

            return null;
        }
    }
}
