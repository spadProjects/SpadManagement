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
    public class GeoDivisionRepository : IRepository<GeoDivision, GeoDivisionSearchObject, Navigations>
    {
        private DatabaseContext db;

        public GeoDivisionRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public GeoDivision Save(GeoDivision entity)
        {
            if (entity.Id == 0)
            {
                db.GeoDivisions.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<GeoDivision>().Attach(entity);
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

        public void Delete(GeoDivision entity)
        {

        }

        public GeoDivision GetEntity(object id)
        {
            return db.GeoDivisions.Find(id);
        }

        public GeoDivision GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<GeoDivision> GetDefaultQuery()
        {
            var result = db.GeoDivisions;
            return result.AsQueryable();
        }
        public IQueryable<GeoDivision> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<GeoDivision> GetDefaultQuery(GeoDivisionSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<GeoDivision> GetDefaultQuery(GeoDivisionSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<GeoDivision> GetDefaultQuery(GeoDivisionSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<GeoDivision> GetDefaultQuery(GeoDivisionSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
