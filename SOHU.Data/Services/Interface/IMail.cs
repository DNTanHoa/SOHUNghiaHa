using SOHU.Services.Implement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Services.Interface
{
    public interface IMail
    {
        public void SendMail(Mail mail);
    }
}
