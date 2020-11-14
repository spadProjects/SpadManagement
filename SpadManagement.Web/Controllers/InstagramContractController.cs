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
    public class InstagramContractController : Controller
    {
        // GET: Contract
        public ActionResult Index()
        {
            var hasAccess = AuthorizationManager.HasAccess("InstagramContract.Index");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            return View();
        }
        
        #region CRUD
        public ActionResult Create()
        {
            var hasAccess = AuthorizationManager.HasAccess("InstagramContract.Create");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            var model = new ContractViewModel();
            model.States = GeoDivisionService.GetInstance().GetGeoDivisionByType(GeoDivisionTypes.State);
            model.Customers = CustomerService.GetInstance().GetDefaultQuery().ToList();
            model.Persons = PersonService.GetInstance().GetDefaultQuery().ToList();

            return View(model);
        }

        public ActionResult View(int id)
        {
            var hasAccess = AuthorizationManager.HasAccess("InstagramContract.View");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            var total = 0;
            LogManagement.Logging($"View InstagramContract id:{id}", (int)LogType.Info, "View InstagramContract", "InstagramContractController/ViewAction");
            var model = InstagramContractService.GetInstance().GetDefaultQuery(new InstagramContractSearchObject(),
                new List<Navigations> { Navigations.person, Navigations.City }, out total)
                .Where(w => w.Id == id).FirstOrDefault();

            var planDpService = PlanDurationPriceService.GetInstance();
            var planTypeService = PlanTypeService.GetInstance();

            model.InstagramContractPlans.ToList().ForEach(item =>
            {
                var ptId = planDpService.GetEntity(item.PlanDurationPriceId).PlanTypeId;
                item.PlanTitle = planTypeService.GetEntity(ptId).PlanTitle;
            });

            return View(model);
        }

        public ActionResult Update(int id)
        {
            var hasAccess = AuthorizationManager.HasAccess("InstagramContract.Update");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            LogManagement.Logging($"update InstagramContract id:{id}", (int)LogType.Info, "Update InstagramContract", "InstagramContractController/UpdateAction");
            var model = InstagramContractService.GetInstance().GetEntity(id);

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
            var hasAccess = AuthorizationManager.HasAccess("InstagramContract.Delete");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            LogManagement.Logging($"Delete InstagramContract id:{id}", (int)LogType.Info, "Delete InstagramContract", "InstagramContractController/DeleteAction");

            var errorMessage = "";

            try
            {
                InstagramContractService.GetInstance().Delete(id);
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
        public ActionResult Submit(InstagramContract entity)
        {
            var modeTitle = entity.Id != 0 ? $"Update InstagramContract id:{entity.Id}" : "Create InstagramContract";
            LogManagement.Logging($"modeTitle", (int)LogType.Info, "Create/Update InstagramContract", "InstagramContractController/SubmitAction");

            var errorMessage = "";

            //var x = entity.Password;
            if (entity.ContractDate != null)
                entity.ContractDate = Utility.ConvertToPersian(entity.ContractDate.ToString());
            //entity.ContractDate = DateTime.Now;
            //if (entity.FromDate != null)
            //    entity.FromDate = Utility.ConvertToPersian(entity.FromDate.ToString());
            //if (entity.ToDate != null)
            //    entity.ToDate = Utility.ConvertToPersian(entity.ToDate.ToString());


            var contractService = InstagramContractService.GetInstance();
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

            var searchObject = new InstagramContractSearchObject
            {

            };

            LogManagement.Logging($"Search InstagramContract from list", (int)LogType.Info, "Search InstagramContract", "InstagramContractController/SearchAction");
            var data = GetAllContracts(searchObject);

            return Json(data, JsonRequestBehavior.AllowGet); //message
        }

        public ContractGrid GetAllContracts(InstagramContractSearchObject searchObject)
        {
            var gridPageSize = searchObject.PageSize ?? Utility.PageSize;

            var contractService = InstagramContractService.GetInstance();

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
                            InstagramId = c.InstagramId,
                            PaymentTotalPriceStr = c.PaymentTotalPrice.ToString("N0", new NumberFormatInfo()
                            {
                                NumberGroupSizes = new[] { 3 },
                                NumberGroupSeparator = ","
                            }),
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
        public ActionResult SaveContractContext(InstagramContract entity)
        {
            LogManagement.Logging("update Instagramcontext", (int)LogType.Info, "Update InstagramContractContext", "InstagramContractController/SaveInstagramContractContext");

            var errorMessage = "";

            try
            {
                InstagramContractService.GetInstance().SaveContext(entity.ContractContext);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return Json(errorMessage, JsonRequestBehavior.AllowGet); //message
        }

        public ActionResult Print(int id)
        {
            var hasAccess = AuthorizationManager.HasAccess("InstagramContract.Print");
            if (!hasAccess)
                throw new Exception("شما مجاز به انجام این عملیات نیستید");

            //LogService.Logging($"Print Contract id:{id}", (int)LogType.Info, "print to pdf", "ContractController/PrintAction");
            StiReport report = new StiReport();
            report.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reports/InstagramContract.mrt"));

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
            var Contract = InstagramContractService.GetInstance().GetDefaultQuery(new InstagramContractSearchObject(), out total)
                .Where(w => w.Id == id).FirstOrDefault();

            var geoDivisionService = GeoDivisionService.GetInstance();

            var city = geoDivisionService.GetDefaultQuery().Where(w => w.Id == Contract.CustomerCityId).FirstOrDefault();
            var state = geoDivisionService.GetDefaultQuery().Where(w => w.Id == city.ParentId).FirstOrDefault();

            var personName = PersonService.GetInstance().GetEntity(Contract.PersonId ?? 0)?.FullName;

            var obj = new ContractDTO
            {
                ContractNo = Contract.ContractNo.ToString(),
                CustomerCity = city.Title,
                PlanDescription = HtmlConvert(Contract.ContractPlanDescriptions),
                CustomerManagerName = Contract.CustomerManagerName,
                CustomerMobile = Contract.CustomerMobile,
                CustomerName = Contract.CustomerName,
                InstagramId = Contract.InstagramId,
                CustomerState = state.Title,
                PersonName = personName,
                DiscountPriceStr = Contract.DiscountTotalPrice.ToString("N0", new NumberFormatInfo()
                {
                    NumberGroupSizes = new[] { 3 },
                    NumberGroupSeparator = ","
                }),
                PaymentPriceStr = Contract.PaymentTotalPrice.ToString("N0", new NumberFormatInfo()
                {
                    NumberGroupSizes = new[] { 3 },
                    NumberGroupSeparator = ","
                }),
                ContractDate = Contract.ContractDate.ToPersianString(),
                ContractContext = HtmlConvert(Contract.ContractContext),
                TotalPriceStr = Contract.TotalPrice.ToString("N0", new NumberFormatInfo()
                {
                    NumberGroupSizes = new[] { 3 },
                    NumberGroupSeparator = ","
                }),
            };

            var contractPlans = new List<ContractPlanDTO>();
            var rowIndex = 0;

            Contract.InstagramContractPlans.ToList().ForEach(item =>
            {
                rowIndex++;

                var planTypeId = PlanDurationPriceService.GetInstance().GetDefaultQuery()
                            .Where(w => w.Id == item.PlanDurationPriceId).Select(s => s.PlanTypeId).FirstOrDefault();

                var planTitle = PlanTypeService.GetInstance().GetEntity(planTypeId).PlanTitle;

                var objItem = new ContractPlanDTO
                {
                    DiscountStr = item.Discount.ToString("N0", new NumberFormatInfo()
                    {
                        NumberGroupSizes = new[] { 3 },
                        NumberGroupSeparator = ","
                    }),
                    DurationTitle = item.DurationTitle,
                    PersianStartDate = item.StartDate.ToPersianString(),
                    PlanTitle = planTitle,
                    RowIndex = rowIndex.ToString(),
                    TotalPriceStr = item.TotalPrice.ToString("N0", new NumberFormatInfo()
                    {
                        NumberGroupSizes = new[] { 3 },
                        NumberGroupSeparator = ","
                    }),
                    PriceStr = item.Price.ToString("N0", new NumberFormatInfo()
                    {
                        NumberGroupSizes = new[] { 3 },
                        NumberGroupSeparator = ","
                    }),
                };

                contractPlans.Add(objItem);
            });

            obj.ContractPlans = contractPlans;

            return obj;
        }

        private string HtmlConvert(string html)
        {
            return html.Replace("&nbsp;", " ").Replace("\t", "").Replace("\n", "");
        }
    }
}
