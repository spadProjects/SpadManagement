using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.DTO
{
    public class ContractPlanDTO
    {
        public string PlanTitle { get; set; }
        public string PersianStartDate { get; set; }
        public string DurationTitle { get; set; }
        public string PriceStr { get; set; }
        public string DiscountStr { get; set; }
        public string TotalPriceStr { get; set; }
        public string RowIndex { get; set; }
    }
}
