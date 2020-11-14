using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.DTO
{
    public class ContractDTO
    {
        public string ContractNo { get; set; }
        public string ContractDate { get; set; }
        public string CustomerMobile { get; set; }
        public string TotalPriceStr { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string InstagramId { get; set; }
        public string DomainId { get; set; }
        public string CustomerName { get; set; }
        public string PersonName { get; set; }
        public string PaymentPriceStr { get; set; }
        public string DiscountPriceStr { get; set; }
        public string CustomerManagerName { get; set; }
        public string ContractContext { get; set; }
        public string PlanDescription { get; set; }

        private List<ContractPlanDTO> _contractPlans;
        public List<ContractPlanDTO> ContractPlans
        {
            get { return _contractPlans ?? new List<ContractPlanDTO>(); }
            set { _contractPlans = value; }
        }
    }
}
