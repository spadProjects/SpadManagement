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
    public class AccountRepository : IRepository<Account, AccountSearchObject, Navigations>
    {
        private DatabaseContext db;

        public AccountRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public Account Save(Account entity)
        {
            if (entity.Id == 0)
            {
                db.Accounts.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<Account>().Attach(entity);
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

        public void Delete(Account entity)
        {

        }

        public Account GetEntity(object id)
        {
            return db.Accounts.Find(id);
        }

        public Account GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<Account> GetDefaultQuery()
        {
            var result = db.Accounts;
            return result.AsQueryable();
        }
        public IQueryable<Account> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<Account> GetDefaultQuery(AccountSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<Account> GetDefaultQuery(AccountSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<Account> GetDefaultQuery(AccountSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<Account> GetDefaultQuery(AccountSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
