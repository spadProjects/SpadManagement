using SpadManagement.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadManagement.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomer(int id)
        {
            var customer = CustomerService.GetInstance().GetEntity(id);
            var stateId = GeoDivisionService.GetInstance().GetEntity(customer?.CityId ?? 0).ParentId;
            var customerData = new { Id = customer.Id, ManagerName = customer.ManagerName,
                                     CityId = customer.CityId, StateId = stateId, Mobile = customer.Mobile };           

            return Json(customerData, JsonRequestBehavior.AllowGet);
        }

    }
}