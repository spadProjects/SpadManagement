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
    public class WebsiteContractItemService : BaseService<WebsiteContractItem, int>
    {
        private static WebsiteContractItemService _instance;
        private WebsiteContractItemRepository _repository;

        public WebsiteContractItemService()
        {
            _repository = new WebsiteContractItemRepository();
        }

        public static WebsiteContractItemService GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new WebsiteContractItemService();
            //}

            return _instance;
        }

        public WebsiteContractItem Save(WebsiteContractItem entity)
        {
            SetLogInfo(entity);

            var result = _repository.Save(entity);

            return entity;
        }

        public WebsiteContractItem GetEntity(int id)
        {
            return _repository.GetEntity(id);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetEntity(id);

            _repository.Delete(entity);
        }
        public List<WebsiteContractItem> GetDefaultQuery(ContractPlanSearchObject searchObbject, out int totalCount)
        {
            return _repository.GetDefaultQuery(searchObbject, out totalCount).ToList();
        }
    }
}
