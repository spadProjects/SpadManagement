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
    public class PlanDurationPriceService : BaseService<PlanDurationPrice, int>
    {
        private static PlanDurationPriceService _instance;
        private PlanDurationPriceRepository _repository;

        public PlanDurationPriceService()
        {
            _repository = new PlanDurationPriceRepository();
        }

        public static PlanDurationPriceService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new PlanDurationPriceService();
            //}

            return _instance;
        }

        public PlanDurationPrice Save(PlanDurationPrice entity)
        {
            SetLogInfo(entity);

            var result = _repository.Save(entity);

            return entity;
        }

        public PlanDurationPrice GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }

        public IQueryable<PlanDurationPrice> GetDefaultQuery()
        {
            return _repository.GetDefaultQuery();
        }

        public List<PlanDurationPrice> GetDefaultQuery(PlanDurationPriceSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
    }
}
