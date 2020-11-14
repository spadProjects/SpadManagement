using SpadManagement.Common;
using SpadManagement.DataAccess.Common;
using SpadManagement.Infrastructure.Authorization;
using SpadManagement.Infrastructure.Logging;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using SpadManagement.Service.Services;
using SpadManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadManagement.Web.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            //var hasAccess = AuthorizationManager.HasAccess("Customers.Index");
            //if (!hasAccess)
            //    throw new Exception("شما مجاز به انجام این عملیات نیستید");

            return View();
        }
        public ActionResult Search(int? pageNumber = 1)
        {

            var searchObject = new CustomerSearchObject
            {

            };

            LogManagement.Logging($"Search Customers from list", (int)LogType.Info, "Search Customers", "CustomersController/SearchAction");
            var data = GetAllCustomers(searchObject);

            return Json(data, JsonRequestBehavior.AllowGet); //message
        }

        public CustomerGrid GetAllCustomers(CustomerSearchObject searchObject)
        {
            var gridPageSize = searchObject.PageSize ?? Utility.PageSize;

            var customerService = CustomerService.GetInstance();

            int totalCount = 0;
            var navigationList = new List<Navigations>
            {
               Navigations.City,
            };
            var data = (from c in customerService.GetDefaultQuery(searchObject, navigationList,out totalCount)
                        select new CustomerViewModel
                        {
                            Id = c.Id,
                            FullName = c.FullName,
                            ManagerName = c.ManagerName,
                            Mobile = c.Mobile,
                            City = c.City,
                        });

            var gridData = new CustomerGrid
            {
                CustomerList = data.ToList(),
                PageCount = Utility.CalculatePageSize(totalCount, gridPageSize),
                PageSize = gridPageSize
            };

            return gridData;
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(Customer entity)
        {
            var modeTitle = entity.Id != 0 ? $"Update Customer id:{entity.Id}" : "Create Customer";
            LogManagement.Logging($"modeTitle", (int)LogType.Info, "Create/Update Customer", "CustomersController/SubmitAction");

            var errorMessage = "";



            var customerService = CustomerService.GetInstance();
            try
            {
                customerService.Save(entity);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return Json(errorMessage, JsonRequestBehavior.AllowGet); //message
        }
        #region crud
        public ActionResult Create()
        {
            //var hasAccess = AuthorizationManager.HasAccess("Customers.Create");
            //if (!hasAccess)
            //    throw new Exception("شما مجاز به انجام این عملیات نیستید");

            var model = new CustomerViewModel();
            model.States = GeoDivisionService.GetInstance().GetGeoDivisionByType(GeoDivisionTypes.State);

            return View(model);
        }
        #endregion
    }
}