using SpadManagement.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadManagement.Web.Controllers
{
    public class PlanDurationPriceController : Controller
    {
        // GET: PlanDurationPrice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPlanDurationPrice(int id)
        {
            var planType = PlanDurationPriceService.GetInstance().GetDefaultQuery().OrderBy(p => p.DisplayOrder)
                .Where(w => w.Id == id).Select(s => new
                {
                    Id = s.Id,
                    Price = s.Price,
                    Duration = s.Duration,
                    PlanDescription = s.PlanDescription
                }).FirstOrDefault();

            return Json(planType, JsonRequestBehavior.AllowGet); //message
        }

        public ActionResult GetPlanDurationPrices(int id)
        {
            var planType = PlanDurationPriceService.GetInstance().GetDefaultQuery().OrderBy(p => p.DisplayOrder)
                .Where(w => w.PlanTypeId == id).Select(s => new { Id = s.Id, Price = s.Price, Duration = s.Duration,
                    PlanDescription = s.PlanDescription
                }).ToList();

            return Json(planType, JsonRequestBehavior.AllowGet); //message
        }
    }
}