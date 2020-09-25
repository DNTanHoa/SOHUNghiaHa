using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace NghiaHa.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult<string> Detail(int ID)
        {
            var model = customerRepository.GetByID(ID);
            return ObjectToJson(model);
        }

        [HttpPost]
        public ActionResult<string> Create(Customer model)
        {
            model.Initialization(InitType.Insert, RequestUserID);
            string note = AppGlobal.InitString;
            var result = customerRepository.Create(model);
            if(result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return ObjectToJson(note);
        }

        [HttpPut]
        public ActionResult<string> Update(Customer model)
        {
            model.Initialization(InitType.Insert, RequestUserID);
            string note = AppGlobal.InitString;
            var result = customerRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return ObjectToJson(note);
        }

        [HttpDelete]
        public ActionResult<string> Delete(int ID)
        {
            string note = AppGlobal.InitString;
            var result = customerRepository.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return ObjectToJson(note);
        }

        [HttpGet]
        public ActionResult<string> GetAllToList()
        {
            return ObjectToJson(customerRepository.GetAllToList());
        }
    }
}
