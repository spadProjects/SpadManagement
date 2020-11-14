using SpadManagement.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadManagement.Web.Controllers
{
    public class GeoDivisionController : Controller
    {
        // GET: GeoDivision
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCities(int id)
        {
            var cities = GeoDivisionService.GetInstance().GetGeoDivisionByType(Common.GeoDivisionTypes.City)
                .Where(w => w.ParentId == id).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet); //message
        }
    }
}