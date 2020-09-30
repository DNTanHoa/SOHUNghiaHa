using SOHU.Data.Models;

namespace SOHU.Data.ModelExtensions
{
    public static class InvoiceExtension
    {
        public static void TrimInvoiceCode(this Invoice model)
        {
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                model.InvoiceCode = model.InvoiceCode.Trim();
            }
        }

        public static void TrimModel(this Invoice model)
        {
            if (!string.IsNullOrEmpty(model.InvoiceCode))
            {
                model.InvoiceCode = model.InvoiceCode.Trim();
            }
            if (!string.IsNullOrEmpty(model.InvoiceName))
            {
                model.InvoiceName = model.InvoiceName.Trim();
            }
            if (!string.IsNullOrEmpty(model.BuyPhone))
            {
                model.BuyPhone = model.BuyPhone.Trim();
            }
            if (!string.IsNullOrEmpty(model.BuyAddress))
            {
                model.BuyAddress = model.BuyAddress.Trim();
            }
            if (!string.IsNullOrEmpty(model.HangMuc))
            {
                model.HangMuc = model.HangMuc.Trim();
            }
            if (!string.IsNullOrEmpty(model.HopDongTitle))
            {
                model.HopDongTitle = model.HopDongTitle.Trim();
            }
            if (!string.IsNullOrEmpty(model.HopDongTitleSub))
            {
                model.HopDongTitleSub = model.HopDongTitleSub.Trim();
            }
        }
    }
}
