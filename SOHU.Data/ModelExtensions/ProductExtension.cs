using SOHU.Data.Models;

namespace SOHU.Data.ModelExtensions
{
    public static class ProductExtension
    {
        public static void TrimTitle(this Product model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
        }
    }
}
