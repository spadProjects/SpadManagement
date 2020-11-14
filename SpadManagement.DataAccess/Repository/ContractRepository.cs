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
    public class ContractRepository : IRepository<Contract, ContractSearchObject, Navigations>
    {
        private DatabaseContext db;

        public ContractRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public Contract Save(Contract entity)
        {
            var result = db.Contracts.Add(entity);
            db.SaveChanges();

            return result;
        }

        public void Delete(object id)
        {
            var entity = GetEntity(id);
            Delete(entity);
        }

        public void Delete(Contract entity)
        {

        }

        public Contract GetEntity(object id)
        {
            return db.Contracts.Find(id);
        }

        public Contract GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<Contract> GetDefaultQuery()
        {
            var result = db.Contracts.ToList();




            return result.AsQueryable();
        }
        public IQueryable<Contract> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<Contract> GetDefaultQuery(ContractSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<Contract> GetDefaultQuery(ContractSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<Contract> GetDefaultQuery(ContractSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<Contract> GetDefaultQuery(ContractSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
