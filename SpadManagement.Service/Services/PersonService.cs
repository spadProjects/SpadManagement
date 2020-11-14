using SpadManagement.DataAccess.Repository;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Service.Services
{
    public class PersonService : BaseService<Person, int>
    {
        private static PersonService _instance;
        private PersonRepository _repository;

        public PersonService()
        {
            _repository = new PersonRepository();
        }

        public static PersonService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new PersonService();
            //}

            return _instance;
        }

        public Person Save(Person entity)
        {
            SetLogInfo(entity);

            var result = _repository.Save(entity);

            return entity;
        }

        public Person GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<Person> GetDefaultQuery()
        {
            return _repository.GetDefaultQuery().ToList();
        }
        public List<Person> GetDefaultQuery(PersonSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
    }
}
