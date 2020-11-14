using FluentValidation.Results;
using SpadManagement.Common;
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
    public class ContractService : BaseService<Contract, int>
    {
        private static ContractService _instance;
        private ContractRepository _repository;

        public ContractService()
        {
            _repository = new ContractRepository();
        }

        public static ContractService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new ContractService();
            //}

            return _instance;
        }

        public Contract Save(Contract entity)
        {
            if (entity.Id == 0)
                entity.ContractNo = GenerateContractNo();

            var errorList = new List<ValidationFailure>();

            entity.ContractContext = new SystemParameterRepository().GetEntity(SystemParameterCodes.InstagramContractContext)?.Value;

            SetLogInfo(entity);
            var errorEntity = ContractValidator.GetInstance().Validate(entity);
            errorList.AddRange(errorEntity.Errors);

            //entity.instagramContractPlans.ToList().ForEach(item =>
            //{
            //    var error = ContractPlanValidator.GetInstance().Validate(item);
            //    ContractPlanService.GetInstance().SetLogInfo(item);

            //    item.StartDate = DateTime.Now;
            //    item.EndDate = DateTime.Now;

            //    errorList.AddRange(error.Errors);
            //});

            if (errorList.Count != 0)
            {
                throw new SpadException(string.Join(",", errorList.Select(s => s.ErrorMessage)));
            }


            var result = _repository.Save(entity);

            return entity;
        }

        public Contract GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<Contract> GetDefaultQuery(ContractSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }

        private string GenerateContractNo()
        {
            var total = 0;
            var lastNo = GetDefaultQuery(new ContractSearchObject(), out total).OrderByDescending(p => p.Id)
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
            var sysparam = sysParamRepos.GetEntity(SystemParameterCodes.ContractContext);

            sysparam.Value = context;

            sysParamRepos.Save(sysparam);
        }
    }
}
