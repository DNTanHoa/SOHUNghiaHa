using SOHU.Data.Models;

namespace SOHU.Data.ModelExtensions
{
    public static class ConfigExtension
    {
        public static void TrimModel(this Config model)
        {
            if (!string.IsNullOrEmpty(model.CodeName))
            {
                model.CodeName = model.CodeName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Note))
            {
                model.Note = model.Note.Trim();
            }
        }
    }
}
