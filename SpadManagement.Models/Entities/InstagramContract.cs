using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class InstagramContract : Contract
    {
        [Required]
        [StringLength(300)]
        public string InstagramId { get; set; }
        public virtual ICollection<InstagramContractPlan> InstagramContractPlans { get; set; }
    }
}

