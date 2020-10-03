using SOHU.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Mobile.Services.Invoice
{
    public interface IInvoiceService
    {
        List<Project> GetProjects(int year, int month);

        Project GetByID(int ID);
    }
}
