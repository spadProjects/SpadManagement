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
    public class WebsiteContractRepository : IRepository<WebsiteContract, WebsiteContractSearchObject, Navigations>
    {
        private DatabaseContext db;

        public WebsiteContractRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public WebsiteContract Save(WebsiteContract entity)
        {
            if (entity.Id == 0)
            {
                db.WebsiteContracts.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<WebsiteContract>().Attach(entity);
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

        public void Delete(WebsiteContract entity)
        {

        }

        public WebsiteContract GetEntity(object id)
        {
            return db.WebsiteContracts.Find(id);
        }

        public WebsiteContract GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<WebsiteContract> GetDefaultQuery()
        {
            var result = db.WebsiteContracts;
            return result.AsQueryable();
        }
        public IQueryable<WebsiteContract> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<WebsiteContract> GetDefaultQuery(WebsiteContractSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<WebsiteContract> GetDefaultQuery(WebsiteContractSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<WebsiteContract> GetDefaultQuery(WebsiteContractSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<WebsiteContract> GetDefaultQuery(WebsiteContractSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;

            var resultQuery = GetDefaultQuery();

            if (navigations.Contains(Navigations.person))
                resultQuery.Include(p => p.Person);

            return resultQuery;
        }
        #endregion

        #region Methods
        #endregion
    }
}
