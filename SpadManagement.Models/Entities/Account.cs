using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class Account : EntityBase<int>
    {
        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [Required]
        [StringLength(100)]
        public string OwnerName { get; set; }

        [StringLength(100)]
        public string AccountNo { get; set; }

        [StringLength(100)]
        public string CardNo { get; set; }

        [StringLength(100)]
        public string ShebaNo { get; set; }

        public virtual ICollection<WebsiteContract> WebsiteContracts { get; set; }
    }
}
