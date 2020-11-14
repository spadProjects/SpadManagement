using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpadManagement.Web.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LastPasswordChangedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastLoginDatePreview { get; set; }
        public bool? IsLocked { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnectionString")
        {
        }
    }
}