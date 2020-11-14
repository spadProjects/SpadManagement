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
    public class PlanTypeService : BaseService<PlanType, int>
    {
        private static PlanTypeService _instance;
        private PlanTypeRepository _repository;

        public PlanTypeService()
        {
            _repository = new PlanTypeRepository();
        }

        public static PlanTypeService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new PlanTypeService();
            //}

            return _instance;
        }

        public PlanType Save(PlanType entity)
        {
            SetLogInfo(entity);

            var result = _repository.Save(entity);

            return entity;
        }

        public PlanType GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }

        public IQueryable<PlanType> GetDefaultQuery()
        {
            return _repository.GetDefaultQuery();
        }

        public List<PlanType> GetDefaultQuery(PlanTypeSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
    }
}
