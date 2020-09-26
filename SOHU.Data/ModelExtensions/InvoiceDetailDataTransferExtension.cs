using SOHU.Data.DataTransferObject;

namespace SOHU.Data.ModelExtensions
{
    public static class InvoiceDetailDataTransferExtension
    {
        public static void InitializationDataTransfer(this InvoiceDetailDataTransfer model)
        {
            model.ProductID = model.Product.ID;
            model.UnitID = model.Unit.ID;
            model.Total = model.UnitPrice * model.Quantity;
            model.Total01 = model.UnitPrice * model.Quantity01;
        }

        public static void InitializationInvoiceDetailDataTransfer(this InvoiceDetailDataTransfer model)
        {
            if (model.Product != null)
            {
                model.ProductID = model.Product.ID;
            }
            if (model.Unit != null)
            {
                model.UnitID = model.Unit.ID;
            }
            model.Total = model.UnitPrice * model.Quantity;
        }
    }
}
