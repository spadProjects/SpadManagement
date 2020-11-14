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
    public class Customer : EntityBase<int>
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ManagerName { get; set; }

        public int CityId { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [NotMapped]
        public int? StateId { get; set; }

        public virtual GeoDivision City { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
