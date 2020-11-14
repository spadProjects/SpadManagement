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
    public class PersonRepository : IRepository<Person, PersonSearchObject, Navigations>
    {
        private DatabaseContext db;

        public PersonRepository()
        {
            db = DatabaseContext.GetInstance();
        }

        #region IService
        public Person Save(Person entity)
        {
            if (entity.Id == 0)
            {
                db.People.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<Person>().Attach(entity);
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

        public void Delete(Person entity)
        {

        }

        public Person GetEntity(object id)
        {
            return db.People.Find(id);
        }

        public Person GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<Person> GetDefaultQuery()
        {
            var result = db.People;
            return result.AsQueryable();
        }
        public IQueryable<Person> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<Person> GetDefaultQuery(PersonSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<Person> GetDefaultQuery(PersonSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<Person> GetDefaultQuery(PersonSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<Person> GetDefaultQuery(PersonSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
