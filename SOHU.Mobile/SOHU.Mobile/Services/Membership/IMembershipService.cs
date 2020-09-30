using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Services.Membership
{
    public interface IMembershipService
    {
        public bool Login(string account, string password, out string token);
    }
}
