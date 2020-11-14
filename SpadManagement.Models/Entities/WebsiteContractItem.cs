using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class WebsiteContractItem : EntityBase<int>
    {
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsPrePayment { get; set; }
        public int DisplayOrder { get; set; }
        public virtual Contract Contract { get; set; }
    }
}
