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
    public class InstagramContractService : BaseService<InstagramContract, int>
    {
        private static InstagramContractService _instance;
        private InstagramContractRepository _repository;

        public InstagramContractService()
        {
            _repository = new InstagramContractRepository();
        }

        public static InstagramContractService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new InstagramContractService();
            //}

            return _instance;
        }

        public InstagramContract Save(InstagramContract entity)
        {
            if (entity.Id == 0)
                entity.ContractNo = GenerateContractNo();

            var errorList = new List<ValidationFailure>();

            entity.ContractContext = new SystemParameterRepository().GetEntity(SystemParameterCodes.InstagramContractContext)?.Value;

            SetLogInfo(entity);
            var errorEntity = ContractValidator.GetInstance().Validate(entity);
            errorList.AddRange(errorEntity.Errors);

            entity.InstagramContractPlans.ToList().ForEach(item =>
            {
                var error = InstagramContractPlanValidator.GetInstance().Validate(item);
                InstagramContractPlanService.GetInstance().SetLogInfo(item);

                var dur = Convert.ToInt32(item.DurationTitle.Replace("ماه", "").Trim());

                if (item.StartDate != null)
                    item.StartDate = Utility.ConvertToPersian(item.StartDate.ToString());

                //item.StartDate = DateTime.Now;
                item.EndDate = item.StartDate.AddMonths(dur);

                errorList.AddRange(error.Errors);
            });

            if (errorList.Count != 0)
            {
                throw new SpadException(string.Join(",", errorList.Select(s => s.ErrorMessage)));
            }


            var result = _repository.Save(entity);

            return entity;
        }

        public InstagramContract GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<InstagramContract> GetDefaultQuery(InstagramContractSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, new List<Navigations> { Navigations.person, Navigations.City }
            , out totalCount).ToList();
        }
        
        public IEnumerable<InstagramContract> GetDefaultQuery(InstagramContractSearchObject instagramContractSearchObject, 
                List<Navigations> list, out int total)
        {
            return _repository.GetDefaultQuery(instagramContractSearchObject, list , out total).ToList();
        }

        private string GenerateContractNo()
        {
            var total = 0;
            var lastNo = GetDefaultQuery(new InstagramContractSearchObject(), out total).OrderByDescending(p => p.Id)
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
