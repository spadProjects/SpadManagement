using SpadManagement.DataAccess.Common;
using SpadManagement.DataAccess.Context;
using SpadManagement.Interfaces;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.DataAccess.Repository
{
    public class InstagramContractRepository : IRepository<InstagramContract, InstagramContractSearchObject, Navigations>
    {
        private DatabaseContext db;

        public InstagramContractRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public InstagramContract Save(InstagramContract entity)
        {
            if (entity.Id == 0)
            {
                db.InstagramContracts.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<InstagramContract>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return entity;
        }

        public void Delete(object id)
        {
            var entity = GetEntity(id);
            Delete(entity);
        }

        public void Delete(InstagramContract entity)
        {

        }

        public InstagramContract GetEntity(object id)
        {
            return db.InstagramContracts.Find(id);
        }

        public InstagramContract GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<InstagramContract> GetDefaultQuery()
        {
            var result = db.InstagramContracts;
            return result.AsQueryable();
        }
        public IQueryable<InstagramContract> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<InstagramContract> GetDefaultQuery(InstagramContractSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<InstagramContract> GetDefaultQuery(InstagramContractSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<InstagramContract> GetDefaultQuery(InstagramContractSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<InstagramContract> GetDefaultQuery(InstagramContractSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;

            var resultQuery = GetDefaultQuery();

            resultQuery.Include(p => p.InstagramContractPlans);

            if (navigations.Contains(Navigations.person))
                resultQuery.Include(p => p.Person);

            return resultQuery;
        }
        #endregion

        #region Methods
        #endregion
    }
}
