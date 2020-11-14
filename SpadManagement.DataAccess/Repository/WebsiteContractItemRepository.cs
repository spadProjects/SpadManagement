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
    public class WebsiteContractItemRepository : IRepository<WebsiteContractItem, ContractPlanSearchObject, Navigations>
    {
        private DatabaseContext db;

        public WebsiteContractItemRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public WebsiteContractItem Save(WebsiteContractItem entity)
        {
            var result = db.WebsiteContractItems.Add(entity);
            db.SaveChanges();

            return result;
        }

        public void Delete(object id)
        {
            var entity = GetEntity(id);
            Delete(entity);
        }

        public void Delete(WebsiteContractItem entity)
        {

        }

        public WebsiteContractItem GetEntity(object id)
        {
            return db.WebsiteContractItems.Find(id);
        }

        public WebsiteContractItem GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }
        
        public IQueryable<WebsiteContractItem> GetDefaultQuery()
        {
            return db.WebsiteContractItems.AsQueryable();
        }
        public IQueryable<WebsiteContractItem> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<WebsiteContractItem> GetDefaultQuery(ContractPlanSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<WebsiteContractItem> GetDefaultQuery(ContractPlanSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<WebsiteContractItem> GetDefaultQuery(ContractPlanSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<WebsiteContractItem> GetDefaultQuery(ContractPlanSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
