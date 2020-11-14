using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class WebsiteContract : Contract
    {
        [Required]
        [StringLength(300)]
        public string DomainId { get; set; }
        [StringLength(300)]
        public string CustomerAddress { get; set; }
        [StringLength(300)]
        public string CustomerPhone { get; set; }
        [Required]
        public int ExecuteDuration { get; set; }
        public long DomainRegistrationCost { get; set; }
        public long HostRegistrationCost { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<WebsiteContractItem> WebsiteContractItems { get; set; }
    }
}

