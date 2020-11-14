using SpadManagement.DataAccess.Common;
using SpadManagement.DataAccess.Context;
using SpadManagement.Interfaces;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.DataAccess.Repository
{
    public class ContractPlanRepository : IRepository<ContractPlan, ContractPlanSearchObject, Navigations>
    {
        private DatabaseContext db;

        public ContractPlanRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public ContractPlan Save(ContractPlan entity)
        {
            var result = db.ContractPlans.Add(entity);
            db.SaveChanges();

            return result;
        }

        public void Delete(object id)
        {
            var entity = GetEntity(id);
            Delete(entity);
        }

        public void Delete(ContractPlan entity)
        {

        }

        public ContractPlan GetEntity(object id)
        {
            return db.ContractPlans.Find(id);
        }

        public ContractPlan GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }
        
        public IQueryable<ContractPlan> GetDefaultQuery()
        {
            return db.ContractPlans.AsQueryable();
        }
        public IQueryable<ContractPlan> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<ContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<ContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<ContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<ContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
