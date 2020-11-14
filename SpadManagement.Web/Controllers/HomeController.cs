using SpadManagement.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadManagement.Web.Controllers
{
    [SpadAuthFilter]
    public class HomeController : Controller
    {
        // GET: Home
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult View(int id)
        {
            return View();
        }
        }
    }