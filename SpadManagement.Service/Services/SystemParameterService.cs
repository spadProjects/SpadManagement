﻿using FluentValidation.Results;
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
    public class SystemParameterService : BaseService<SystemParameter, int>
    {
        private static SystemParameterService _instance;
        private SystemParameterRepository _repository;

        public SystemParameterService()
        {
            _repository = new SystemParameterRepository();
        }

        public static SystemParameterService GetInstance()
        {
            //if (_instance == null)
            //{
                _instance = new SystemParameterService();
            //}

            return _instance;
        }

        public SystemParameter Save(SystemParameter entity)
        {
            return entity;
        }

        public SystemParameter GetEntity(string id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<SystemParameter> GetDefaultQuery(SystemParameterSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }        
    }
}
