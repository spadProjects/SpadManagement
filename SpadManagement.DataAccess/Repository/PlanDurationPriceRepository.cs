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
    public class PlanDurationPriceRepository : IRepository<PlanDurationPrice, PlanDurationPriceSearchObject, Navigations>
    {
        private DatabaseContext db;

        public PlanDurationPriceRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public PlanDurationPrice Save(PlanDurationPrice entity)
        {
            if (entity.Id == 0)
            {
                db.PlanDurationPrices.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<PlanDurationPrice>().Attach(entity);
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

        public void Delete(PlanDurationPrice entity)
        {

        }

        public PlanDurationPrice GetEntity(object id)
        {
            return db.PlanDurationPrices.Find(id);
        }

        public PlanDurationPrice GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<PlanDurationPrice> GetDefaultQuery()
        {
            var result = db.PlanDurationPrices;
            return result.AsQueryable();
        }
        public IQueryable<PlanDurationPrice> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<PlanDurationPrice> GetDefaultQuery(PlanDurationPriceSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<PlanDurationPrice> GetDefaultQuery(PlanDurationPriceSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<PlanDurationPrice> GetDefaultQuery(PlanDurationPriceSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<PlanDurationPrice> GetDefaultQuery(PlanDurationPriceSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
