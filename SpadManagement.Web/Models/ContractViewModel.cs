using SpadManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpadManagement.Web.Models
{
    public class ContractGrid
    {
        public List<ContractViewModel> ContractList { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }

    public class ContractViewModel
    {
        public int Id { get; set; }
        public string InstagramId { get; set; }
        public string DomainId { get; set; }
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public DateTime ContractDate { get; set; }
        public string PersianContractDate { get; set; }
        public long TotalPrice { get; set; }
        public string TotalPriceStr { get; set; }
        public string ContractContext { get; set; }
        public string ContractPlanDescriptions { get; set; }
        public long DiscountTotalPrice { get; set; }
        public long PaymentTotalPrice { get; set; }
        public string PaymentTotalPriceStr { get; set; }
        public int CustomerId { get; set; }
        public int CustomerCityId { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerManagerName { get; set; }


        private List<InstagramContractPlan> _contractplans;
        public List<InstagramContractPlan> ContractPlans
        {
            get { return _contractplans ?? new List<InstagramContractPlan>(); }
            set { _contractplans = value; }
        }

        private List<GeoDivision> _cities;
        public List<GeoDivision> Cities
        {
            get { return _cities ?? new List<GeoDivision>(); }
            set { _cities = value; }
        }

        private List<Customer> _customers;
        public List<Customer> Customers
        {
            get { return _customers ?? new List<Customer>(); }
            set { _customers = value; }
        }

        private List<Account> _accounts;
        public List<Account> Accounts
        {
            get { return _accounts ?? new List<Account>(); }
            set { _accounts = value; }
        }

        private List<GeoDivision> _states;
        public List<GeoDivision> States
        {
            get { return _states ?? new List<GeoDivision>(); }
            set { _states = value; }
        }

        private List<Person> _persons;
        public List<Person> Persons
        {
            get { return _persons ?? new List<Person>(); }
            set { _persons = value; }
        }
    }
}