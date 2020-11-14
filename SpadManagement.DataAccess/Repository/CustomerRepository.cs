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
    public class CustomerRepository : IRepository<Customer, CustomerSearchObject, Navigations>
    {
        private DatabaseContext db;

        public CustomerRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public Customer Save(Customer entity)
        {
            if (entity.Id == 0)
            {
                db.Customers.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<Customer>().Attach(entity);
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

        public void Delete(Customer entity)
        {

        }

        public Customer GetEntity(object id)
        {
            return db.Customers.Find(id);
        }

        public Customer GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<Customer> GetDefaultQuery()
        {
            var result = db.Customers;
            return result.AsQueryable();
        }
        public IQueryable<Customer> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<Customer> GetDefaultQuery(CustomerSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<Customer> GetDefaultQuery(CustomerSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<Customer> GetDefaultQuery(CustomerSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<Customer> GetDefaultQuery(CustomerSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            var resultQuery = GetDefaultQuery(searchObject, out totalCount);

            if (navigations.Contains(Navigations.City))
                resultQuery = resultQuery.Include(p => p.City);
            return resultQuery;
        }
        #endregion

        #region Methods
        #endregion
    }
}
