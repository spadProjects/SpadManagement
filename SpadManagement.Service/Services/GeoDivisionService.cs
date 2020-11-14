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
    public class GeoDivisionService : BaseService<GeoDivision, int>
    {
        private static GeoDivisionService _instance;
        private GeoDivisionRepository _repository;

        public GeoDivisionService()
        {
            _repository = new GeoDivisionRepository();
        }

        public static GeoDivisionService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new GeoDivisionService();
            //}

            return _instance;
        }

        public GeoDivision Save(GeoDivision entity)
        {
            return entity;
        }

        public GeoDivision GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }

        public IQueryable<GeoDivision> GetDefaultQuery()
        {
            return _repository.GetDefaultQuery();
        }

        public List<GeoDivision> GetDefaultQuery(GeoDivisionSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }

        public List<GeoDivision> GetGeoDivisionByType(GeoDivisionTypes type)
        {
            return _repository.GetDefaultQuery().Where(w => w.GeoDivisionType == (int)type).ToList();
        }
    }
}
