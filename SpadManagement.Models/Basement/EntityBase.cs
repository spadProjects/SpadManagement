using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.Models.Basement
{
    public class EntityBase<TPrimaryKey> 
    {
        protected TPrimaryKey _id { get; set; }
        public TPrimaryKey Id { get; set; }
        public int? ParentId { get; set; }
        public string InsertUser { get; set; }
        public DateTime InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
    }
}
