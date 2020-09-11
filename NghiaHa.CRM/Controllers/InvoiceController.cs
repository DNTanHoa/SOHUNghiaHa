using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID, int CategoryID)
        {
            var model = invoiceRepository.GetByID(ID);
            return View(model);
        }

        public IActionResult GetAllToList([DataSourceRequest] DataSourceRequest request)
        {
            return Json(invoiceRepository.GetAllToList().ToDataSourceResult(request));
        }
    }
}
