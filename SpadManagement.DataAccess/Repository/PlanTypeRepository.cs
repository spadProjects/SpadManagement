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
    public class PlanTypeRepository : IRepository<PlanType, PlanTypeSearchObject, Navigations>
    {
        private DatabaseContext db;

        public PlanTypeRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public PlanType Save(PlanType entity)
        {
            if (entity.Id == 0)
            {
                db.PlanTypes.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<PlanType>().Attach(entity);
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

        public void Delete(PlanType entity)
        {

        }

        public PlanType GetEntity(object id)
        {
            return db.PlanTypes.Find(id);
        }

        public PlanType GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<PlanType> GetDefaultQuery()
        {
            var result = db.PlanTypes;
            return result.AsQueryable();
        }
        public IQueryable<PlanType> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<PlanType> GetDefaultQuery(PlanTypeSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<PlanType> GetDefaultQuery(PlanTypeSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<PlanType> GetDefaultQuery(PlanTypeSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<PlanType> GetDefaultQuery(PlanTypeSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
