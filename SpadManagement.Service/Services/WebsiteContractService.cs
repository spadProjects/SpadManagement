using FluentValidation.Results;
using SpadManagement.Common;
using SpadManagement.DataAccess.Common;
using SpadManagement.DataAccess.Repository;
using SpadManagement.DataAccess.Validation;
using SpadManagement.Infrastructure.Exceptions;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Service.Services
{
    public class WebsiteContractService : BaseService<WebsiteContract, int>
    {
        private static WebsiteContractService _instance;
        private WebsiteContractRepository _repository;

        public WebsiteContractService()
        {
            _repository = new WebsiteContractRepository();
        }

        public static WebsiteContractService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new WebsiteContractService();
            //}

            return _instance;
        }

        public WebsiteContract Save(WebsiteContract entity)
        {
            if (entity.Id == 0)
                entity.ContractNo = GenerateContractNo();

            var errorList = new List<ValidationFailure>();

            entity.ContractContext = new SystemParameterRepository().GetEntity(SystemParameterCodes.WebsiteContractContext)?.Value;

            SetLogInfo(entity);
            var errorEntity = ContractValidator.GetInstance().Validate(entity);
            errorList.AddRange(errorEntity.Errors);

            entity.WebsiteContractItems.ToList().ForEach(item =>
            {
                var error = WebsiteContractItemValidator.GetInstance().Validate(item);
                WebsiteContractItemService.GetInstance().SetLogInfo(item);

                if (item.Date != null)
                    item.Date = Utility.ConvertToPersian(item.Date.ToString());

                //item.StartDate = DateTime.Now;
                //item.EndDate = item.StartDate.AddMonths(dur);

                errorList.AddRange(error.Errors);
            });

            if (errorList.Count != 0)
            {
                throw new SpadException(string.Join(",", errorList.Select(s => s.ErrorMessage)));
            }


            var result = _repository.Save(entity);

            return entity;
        }

        public WebsiteContract GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<WebsiteContract> GetDefaultQuery(WebsiteContractSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, new List<Navigations> { Navigations.person, Navigations.City }
            , out totalCount).ToList();
        }
        
        public IEnumerable<WebsiteContract> GetDefaultQuery(WebsiteContractSearchObject WebsiteContractSearchObject, 
                List<Navigations> list, out int total)
        {
            return _repository.GetDefaultQuery(WebsiteContractSearchObject, list , out total).ToList();
        }

        private string GenerateContractNo()
        {
            var total = 0;
            var lastNo = GetDefaultQuery(new WebsiteContractSearchObject(), out total).OrderByDescending(p => p.Id)
                .Select(s => s.ContractNo).FirstOrDefault();

            var firstPartOfLastNo = "";

            if (string.IsNullOrEmpty(lastNo))
                lastNo = string.Empty;
            else
                firstPartOfLastNo = lastNo.Substring(0, 4);

            var persianDate = DateTime.Now.ToPersianString().Replace("/", "");
            var firstPartOfNo = persianDate.Substring(2, 4);

            if (firstPartOfLastNo == firstPartOfNo)
            {
                var no = Convert.ToInt32(lastNo.Substring(lastNo.Length - 3, 3));

                no++;
                var code = no.ToString();
                firstPartOfNo += code.PadLeft(3, '0');
                //number.ToString("D" + length);
            }
            else
            {
                firstPartOfNo += "001";
            }

            return firstPartOfNo;
        }

        public void SaveContext(string context)
        {
            var sysParamRepos = new SystemParameterRepository();
            var sysparam = sysParamRepos.GetEntity(SystemParameterCodes.WebsiteContractContext);

            sysparam.Value = context;

            sysParamRepos.Save(sysparam);
        }
    }
}
