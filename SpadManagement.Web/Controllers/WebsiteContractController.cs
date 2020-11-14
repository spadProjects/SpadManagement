using SpadManagement.Common;
using SpadManagement.Infrastructure.Filters;
using SpadManagement.Models.DTO;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using SpadManagement.Service.Services;
using SpadManagement.Web.Models;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpadManagement.Infrastructure.Authorization;
using SpadManagement.Infrastructure.Logging;
using SpadManagement.DataAccess.Common;

namespace SpadManagement.Web.Controllers
{

    [SpadAuthFilter]
    public class WebsiteContractController : Controller
    {
        // GET: Contract
        public ActionResult Index()
        {
            var hasAccess = AuthorizationManager.HasAccess("WebsiteContract.Index");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            return View();
        }


        #region CRUD
        public ActionResult Create()
        {
            var hasAccess = AuthorizationManager.HasAccess("WebsiteContract.Create");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            var model = new ContractViewModel();
            model.States = GeoDivisionService.GetInstance().GetGeoDivisionByType(GeoDivisionTypes.State);
            model.Customers = CustomerService.GetInstance().GetDefaultQuery().ToList();
            model.Persons = PersonService.GetInstance().GetDefaultQuery().ToList();
            model.Accounts = AccountService.GetInstance().GetDefaultQuery().ToList();

            return View(model);
        }

        public ActionResult View(int id)
        {
            var hasAccess = AuthorizationManager.HasAccess("WebsiteContract.View");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            var total = 0;
            LogManagement.Logging($"View WebsiteContract id:{id}", (int)LogType.Info, "View WebsiteContract", "WebsiteContractController/ViewAction");
            var model = WebsiteContractService.GetInstance().GetDefaultQuery(new WebsiteContractSearchObject(),
                new List<Navigations> { Navigations.person, Navigations.City }, out total)
                .Where(w => w.Id == id).FirstOrDefault();

            var planDpService = PlanDurationPriceService.GetInstance();
            var planTypeService = PlanTypeService.GetInstance();

            model.WebsiteContractItems.ToList().ForEach(item =>
            {
                //var ptId = planDpService.GetEntity(item.PlanDurationPriceId).PlanTypeId;
                //item.PlanTitle = planTypeService.GetEntity(ptId).PlanTitle;
            });

            return View(model);
        }

        public ActionResult Update(int id)
        {
            var hasAccess = AuthorizationManager.HasAccess("WebsiteContract.Update");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            LogManagement.Logging($"update WebsiteContract id:{id}", (int)LogType.Info, "Update WebsiteContract", "WebsiteContractController/UpdateAction");
            var model = WebsiteContractService.GetInstance().GetEntity(id);

            //var cityList = CityService.GetInstance().GetDefaultQuery();
            //var cityRegionList = RegionService.GetInstance().GetQueryByCityId(model.City.Id);
            //var jobList = JobService.GetInstance().GetDefaultQuery();
            //var adTypeList = AdTypeService.GetInstance().GetDefaultQuery(); ;

            //ViewBag.Cities = cityList;
            //ViewBag.Jobs = jobList;
            //ViewBag.AdTypes = adTypeList;
            //ViewBag.Regions = cityRegionList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var hasAccess = AuthorizationManager.HasAccess("WebsiteContract.Delete");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            LogManagement.Logging($"Delete WebsiteContract id:{id}", (int)LogType.Info, "Delete WebsiteContract", "WebsiteContractController/DeleteAction");

            var errorMessage = "";

            try
            {
                WebsiteContractService.GetInstance().Delete(id);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return Json(errorMessage, JsonRequestBehavior.AllowGet); //message
        }
        #endregion

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(WebsiteContract entity)
        {
            var modeTitle = entity.Id != 0 ? $"Update WebsiteContract id:{entity.Id}" : "Create WebsiteContract";
            LogManagement.Logging($"modeTitle", (int)LogType.Info, "Create/Update WebsiteContract", "WebsiteContractController/SubmitAction");

            var errorMessage = "";

            //var x = entity.Password;
            if (entity.ContractDate != null)
                entity.ContractDate = Utility.ConvertToPersian(entity.ContractDate.ToString());
            //entity.ContractDate = DateTime.Now;
            //if (entity.FromDate != null)
            //    entity.FromDate = Utility.ConvertToPersian(entity.FromDate.ToString());
            //if (entity.ToDate != null)
            //    entity.ToDate = Utility.ConvertToPersian(entity.ToDate.ToString());


            var contractService = WebsiteContractService.GetInstance();
            try
            {
                contractService.Save(entity);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return Json(errorMessage, JsonRequestBehavior.AllowGet); //message
        }

        public ActionResult Search(int? pageNumber = 1)
        {

            var searchObject = new WebsiteContractSearchObject
            {

            };

            LogManagement.Logging($"Search WebsiteContract from list", (int)LogType.Info, "Search WebsiteContract", "WebsiteContractController/SearchAction");
            var data = GetAllContracts(searchObject);

            return Json(data, JsonRequestBehavior.AllowGet); //message
        }

        public ContractGrid GetAllContracts(WebsiteContractSearchObject searchObject)
        {
            var gridPageSize = searchObject.PageSize ?? Utility.PageSize;

            var contractService = WebsiteContractService.GetInstance();

            int totalCount = 0;

            var data = (from c in contractService.GetDefaultQuery(searchObject, out totalCount)
                        select new ContractViewModel
                        {
                            Id = c.Id,
                            ContractNo = c.ContractNo,
                            PersianContractDate = c.ContractDate.ToPersianString(),
                            CustomerMobile = c.CustomerMobile,
                            CustomerName = c.CustomerName,
                            CustomerManagerName = c.CustomerManagerName,
                            DomainId = c.DomainId,
                            PaymentTotalPriceStr = c.PaymentTotalPrice.SplitDigit()
                        });

            var gridData = new ContractGrid
            {
                ContractList = data.ToList(),
                PageCount = Utility.CalculatePageSize(totalCount, gridPageSize),
                PageSize = gridPageSize
            };

            return gridData;
        }

        public ActionResult ContractContext()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveContractContext(WebsiteContract entity)
        {
            LogManagement.Logging("update context", (int)LogType.Info, "Update WebsiteContractContext", "WebsiteContractController/SaveWebsiteContractContext");

            var errorMessage = "";

            try
            {
                WebsiteContractService.GetInstance().SaveContext(entity.ContractContext);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return Json(errorMessage, JsonRequestBehavior.AllowGet); //message
        }

        public ActionResult Print(int id)
        {
            var hasAccess = AuthorizationManager.HasAccess("WebsiteContract.Print");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            //LogService.Logging($"Print Contract id:{id}", (int)LogType.Info, "print to pdf", "ContractController/PrintAction");
            StiReport report = new StiReport();
            report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports/WebsiteContract.mrt"));

            var ContractDto = GetReportDTO(id);
            report.RegBusinessObject("Contract", ContractDto);

            report["ContractContext"] = ContractDto.ContractContext;
            report["PlanDescription"] = ContractDto.PlanDescription;
            report.Dictionary.Variables.Add("ContractContext", ContractDto.ContractContext);
            report.Dictionary.Variables.Add("PlanDescription", ContractDto.PlanDescription);
            //Stimulsoft.Base.StiFontCollection.AddFontFile(@"~\Fonts\BNazanin.ttf");
            report.Render(false);
            MemoryStream stream = new MemoryStream();

            StiPdfExportSettings settings = new StiPdfExportSettings();
            settings.AutoPrintMode = StiPdfAutoPrintMode.Dialog;

            StiPdfExportService service = new StiPdfExportService();
            service.ExportPdf(report, stream, settings);

            this.Response.Buffer = true;
            this.Response.ClearContent();
            this.Response.ClearHeaders();
            this.Response.ContentType = "application/pdf";
            //this.Response.AddHeader("Content-Disposition", "attachment; filename=\"report.pdf\"");
            this.Response.ContentEncoding = Encoding.UTF8;
            this.Response.AddHeader("Content-Length", stream.Length.ToString());
            this.Response.BinaryWrite(stream.ToArray());
            this.Response.End();

            return View();
        }

        private ContractDTO GetReportDTO(int id)
        {
            var total = 0;
            var Contract = WebsiteContractService.GetInstance().GetDefaultQuery(new WebsiteContractSearchObject(), out total)
                .Where(w => w.Id == id).FirstOrDefault();

            var geoDivisionService = GeoDivisionService.GetInstance();

            var city = geoDivisionService.GetDefaultQuery().Where(w => w.Id == Contract.CustomerCityId).FirstOrDefault();
            var state = geoDivisionService.GetDefaultQuery().Where(w => w.Id == city.ParentId).FirstOrDefault();


            var obj = new ContractDTO
            {
                ContractNo = Contract.ContractNo.ToString(),
                CustomerCity = city.Title,
                CustomerManagerName = Contract.CustomerManagerName,
                CustomerMobile = Contract.CustomerMobile,
                CustomerName = Contract.CustomerName,
                DomainId = Contract.DomainId,
                CustomerState = state.Title,
                DiscountPriceStr = Contract.DiscountTotalPrice.SplitDigit(),
                PaymentPriceStr = Contract.PaymentTotalPrice.SplitDigit(),
                ContractDate = Contract.ContractDate.ToPersianString(),
                ContractContext = FillContextData(Contract),
                TotalPriceStr = Contract.TotalPrice.SplitDigit(),
            };

            var contractPlans = new List<ContractPlanDTO>();

            obj.ContractPlans = contractPlans;

            return obj;
        }

        private string HtmlConvert(string html)
        {
            return html.Replace("&nbsp;", " ").Replace("\t", "").Replace("\n", "");
        }

        private string FillContextData(WebsiteContract entity)
        {
            var result = HtmlConvert(entity.ContractContext);

            result = result.Replace("@#DomainId", entity.DomainId);
            result = result.Replace("@#CustomerName", entity.CustomerName);
            result = result.Replace("@#CustomerMobile", entity.CustomerMobile);
            result = result.Replace("@#CustomerAddress", entity.CustomerAddress);
            result = result.Replace("@#CustomerPhone", entity.CustomerPhone);
            result = result.Replace("@#PaymentTotalPrice", entity.PaymentTotalPrice.SplitDigit());

            var websiteItems = string.Empty;

            entity.WebsiteContractItems.OrderBy(p => p.DisplayOrder).ToList().ForEach(item =>
            {
                if (item.IsPrePayment)
                    websiteItems += $"مرحله{item.DisplayOrder}- پرداخت {Utility.NumberToWords(item.Amount)} تومان به عنوان پیش پرداخت در تاریخ {item.Date.ToPersianString()} پرداخت میگردد" + "<Br/>";
                else
                    websiteItems += $"مرحله{item.DisplayOrder}- پرداخت {Utility.NumberToWords(item.Amount)} تومان در تاریخ {item.Date.ToPersianString()}" + "<Br/>";
            });

            result = result.Replace("@#WebsiteContractItems", websiteItems);

            var account = AccountService.GetInstance().GetEntity(entity.AccountId);
            var accountInfo = $"شماره کارت {account.CardNo} و شماره شبا {account.ShebaNo} به نام {account.OwnerName}";
            result = result.Replace("@#AccountInfo", "<p style=\"font - family:Arial\"><strong>" + accountInfo + "</strong></p>");
            result = result.Replace("@#Duration", entity.ExecuteDuration.ToString());
            result = result.Replace("@#DomainCost", Utility.NumberToWords(entity.DomainRegistrationCost));
            result = result.Replace("@#HostCost", Utility.NumberToWords(entity.HostRegistrationCost));

            return result;
        }
    }
}