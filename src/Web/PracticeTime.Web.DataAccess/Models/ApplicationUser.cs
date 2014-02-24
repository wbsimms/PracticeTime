using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PracticeTime.Web.DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string StudentToken { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        protected bool active = true;

        public ApplicationRole()
        {
            // don't use this.
        }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }

        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }
    }
}
