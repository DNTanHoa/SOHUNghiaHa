using SOHU.Data.Models;

namespace SOHU.Data.ModelExtensions
{
    public static class InvoicePaymentExtension
    {
        public static void TrimModel(this InvoicePayment model)
        {
            if (!string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = model.FullName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = model.Phone.Trim();
            }
        }
    }
}
