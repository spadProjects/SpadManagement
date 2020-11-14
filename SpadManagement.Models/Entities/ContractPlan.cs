using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class ContractPlan : EntityBase<int>
    {
        public int PlanDurationPriceId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public long Price { get; set; }
        public long Discount { get; set; }
        public long TotalPrice { get; set; }

        [StringLength(100)]
        public string PlanTitle { get; set; }

        [StringLength(100)]
        public string DurationTitle { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual PlanDurationPrice PlanDurationPrice { get; set; }
    }
}
