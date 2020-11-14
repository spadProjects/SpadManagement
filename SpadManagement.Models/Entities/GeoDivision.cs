using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class GeoDivision : EntityBase<int>
    {
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public int GeoDivisionType { get; set; }
        
    }
}
