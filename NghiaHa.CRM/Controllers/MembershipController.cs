using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Repositories;

namespace NghiaHa.CRM.Controllers
{
    public class MembershipController : BaseController
    {
        private readonly IMembershipRepository membershipRepository;

        public MembershipController(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult HeaderInfor()
        {
            var member = membershipRepository.GetByID(RequestUserID);
            return PartialView("~/Views/Membership/_HeaderInfor.cshtml", member);
        }

        public ActionResult SidebarInfor()
        {
            var member = membershipRepository.GetByID(RequestUserID);
            return PartialView("~/Views/Membership/_SidebarInfor.cshtml",member);
        }
    }
}
