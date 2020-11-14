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
    public class InstagramContractPlanRepository : IRepository<InstagramContractPlan, ContractPlanSearchObject, Navigations>
    {
        private DatabaseContext db;

        public InstagramContractPlanRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public InstagramContractPlan Save(InstagramContractPlan entity)
        {
            var result = db.InstagramContractPlans.Add(entity);
            db.SaveChanges();

            return result;
        }

        public void Delete(object id)
        {
            var entity = GetEntity(id);
            Delete(entity);
        }

        public void Delete(InstagramContractPlan entity)
        {

        }

        public InstagramContractPlan GetEntity(object id)
        {
            return db.InstagramContractPlans.Find(id);
        }

        public InstagramContractPlan GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }
        
        public IQueryable<InstagramContractPlan> GetDefaultQuery()
        {
            return db.InstagramContractPlans.AsQueryable();
        }
        public IQueryable<InstagramContractPlan> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<InstagramContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<InstagramContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<InstagramContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<InstagramContractPlan> GetDefaultQuery(ContractPlanSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
