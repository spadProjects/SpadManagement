using SpadManagement.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadManagement.Web.Controllers
{
    public class PlanTypeController : Controller
    {
        // GET: PlanType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPlanTypes()
        {
            var planType = PlanTypeService.GetInstance().GetDefaultQuery().OrderBy(p => p.DisplayOrder)
                .Select(s => new { Id = s.Id, PlanTitle = s.PlanTitle }).ToList();
            return Json(planType, JsonRequestBehavior.AllowGet); //message
        }

    }
}