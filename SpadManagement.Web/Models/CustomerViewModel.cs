using SpadManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpadManagement.Web.Models
{
    public class CustomerGrid
    {
        public List<CustomerViewModel> CustomerList { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ManagerName { get; set; }
        public string Mobile { get; set; }
        public int ParentId { get; set; }
        public int CityId { get; set; }
        public GeoDivision City { get; set; }

        //private List<GeoDivision> _cities;
        //public List<GeoDivision> Cities
        //{
        //    get { return _cities ?? new List<GeoDivision>(); }
        //    set { _cities = value; }
        //}
        private List<GeoDivision> _states;
        public List<GeoDivision> States
        {
            get { return _states ?? new List<GeoDivision>(); }
            set { _states = value; }
        }
    }
}