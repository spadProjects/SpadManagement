using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class PlanDurationPrice : EntityBase<int>
    {
        [Required]
        [StringLength(200)]
        public string Duration { get; set; }
        public long Price { get; set; }

        public int DisplayOrder { get; set; }

        public int PlanTypeId { get; set; }
        
        public string PlanDescription { get; set; }

        public virtual PlanType PlanType { get; set; }
        public virtual ICollection<InstagramContractPlan> ContractPlans { get; set; }
    }
}
