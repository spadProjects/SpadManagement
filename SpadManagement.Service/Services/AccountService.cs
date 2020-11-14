using FluentValidation.Results;
using SpadManagement.Common;
using SpadManagement.DataAccess.Repository;
using SpadManagement.DataAccess.Validation;
using SpadManagement.Infrastructure.Exceptions;
using SpadManagement.Models.Entities;
using SpadManagement.Models.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Service.Services
{
    public class AccountService : BaseService<Account, int>
    {
        private static AccountService _instance;
        private AccountRepository _repository;

        public AccountService()
        {
            _repository = new AccountRepository();
        }

        public static AccountService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new AccountService();
            //}

            return _instance;
        }

        public Account Save(Account entity)
        {;

            var errorList = new List<ValidationFailure>();
            
            SetLogInfo(entity);
            var errorEntity = AccountValidator.GetInstance().Validate(entity);
            errorList.AddRange(errorEntity.Errors);

            if (errorList.Count != 0)
            {
                throw new SpadException(string.Join(",", errorList.Select(s => s.ErrorMessage)));
            }


            var result = _repository.Save(entity);

            return entity;
        }

        public Account GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<Account> GetDefaultQuery()
        {
            return _repository.GetDefaultQuery().ToList();
        }

        public List<Account> GetDefaultQuery(AccountSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
    }
}
