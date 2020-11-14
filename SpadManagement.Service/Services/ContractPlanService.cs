using SpadManagement.DataAccess.Repository;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Service.Services
{
    public class ContractPlanService : BaseService<ContractPlan, int>
    {
        private static ContractPlanService _instance;
        private ContractPlanRepository _repository;

        public ContractPlanService()
        {
            _repository = new ContractPlanRepository();
        }

        public static ContractPlanService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new ContractPlanService();
            //}

            return _instance;
        }

        public ContractPlan Save(ContractPlan entity)
        {
            SetLogInfo(entity);

            var result = _repository.Save(entity);

            return entity;
        }

        public ContractPlan GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<ContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
    }
}
