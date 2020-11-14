using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class PlanType : EntityBase<int>
    {
        [Required]
        [StringLength(200)]
        public string PlanTitle { get; set; }
        
        public int DisplayOrder { get; set; }
                
        public virtual ICollection<PlanDurationPrice> PlanDurationPrices { get; set; }
    }
}
