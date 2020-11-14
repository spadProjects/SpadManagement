using SpadManagement.Models.Basement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Entities
{
    public class SystemParameter : EntityBase<int>
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string Value { get; set; }
        public string PersianTitle { get; set; }
        public string Description { get; set; }
        public bool IsSystemic { get; set; }
    }
}
