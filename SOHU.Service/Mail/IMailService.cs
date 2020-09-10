using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Service.Mail
{
    public interface IMailService
    {
        public void Send(Mail mail);
    }
}
