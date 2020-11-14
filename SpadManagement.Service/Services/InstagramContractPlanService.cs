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
    public class InstagramContractPlanService : BaseService<InstagramContractPlan, int>
    {
        private static InstagramContractPlanService _instance;
        private InstagramContractPlanRepository _repository;

        public InstagramContractPlanService()
        {
            _repository = new InstagramContractPlanRepository();
        }

        public static InstagramContractPlanService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new InstagramContractPlanService();
            //}

            return _instance;
        }

        public InstagramContractPlan Save(InstagramContractPlan entity)
        {
            SetLogInfo(entity);

            var result = _repository.Save(entity);

            return entity;
        }

        public InstagramContractPlan GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<InstagramContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
    }
}
