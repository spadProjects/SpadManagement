using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class Contract : EntityBase<int>
    {
        [Required]
        [StringLength(50)]
        public string ContractNo { get; set; }
        [Required]
        public DateTime ContractDate { get; set; }
        public long TotalPrice { get; set; }
        public string ContractContext { get; set; }
        public string ContractPlanDescriptions { get; set; }
        public long DiscountTotalPrice { get; set; }
        public long PaymentTotalPrice { get; set; }
        public int CustomerId { get; set; }
        public int CustomerCityId { get; set; }
        
        [StringLength(50)]
        public string CustomerMobile { get; set; }
        [StringLength(500)]
        public string CustomerManagerName { get; set; }
        [StringLength(500)]
        public string CustomerName { get; set; }
        public int? PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
