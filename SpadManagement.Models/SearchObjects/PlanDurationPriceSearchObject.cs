using SpadManagement.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.SearchObjects
{
    public class PlanDurationPriceSearchObject : ISearchObject
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        
    }
}
