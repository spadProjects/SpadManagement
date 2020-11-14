using SpadManagement.DataAccess.Common;
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
    public class CustomerService : BaseService<Customer, int>
    {
        private static CustomerService _instance;
        private CustomerRepository _repository;

        public CustomerService()
        {
            _repository = new CustomerRepository();
        }

        public static CustomerService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new CustomerService();
            //}

            return _instance;
        }

        public Customer Save(Customer entity)
        {
            SetLogInfo(entity);

            var result = _repository.Save(entity);

            return entity;
        }

        public Customer GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<Customer> GetDefaultQuery()
        {
            return _repository.GetDefaultQuery().ToList();
        }
        public List<Customer> GetDefaultQuery(CustomerSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
        public List<Customer> GetDefaultQuery(CustomerSearchObject searchObbject, List<Navigations> navigations, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, navigations, out totalCount).ToList();
        }
    }
}
