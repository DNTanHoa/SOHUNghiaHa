using SOHU.Data.Models;

namespace SOHU.Data.ModelExtensions
{
    public static class MembershipExtension
    {
        public static void TrimModel(this Membership model)
        {
            if (!string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = model.FullName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = model.Phone.Trim();
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                model.Address = model.Address.Trim();
            }
            if (!string.IsNullOrEmpty(model.TaxCode))
            {
                model.TaxCode = model.TaxCode.Trim();
            }
            if (!string.IsNullOrEmpty(model.CitizenIdentification))
            {
                model.CitizenIdentification = model.CitizenIdentification.Trim();
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                model.Email = model.Email.Trim();
            }
        }

        public static void SetDefaultValue(this Membership model)
        {
            //default username by phone number
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Account = model.Phone;
            }

            //default password
            model.Passport = "1234";
        }
    }
}
