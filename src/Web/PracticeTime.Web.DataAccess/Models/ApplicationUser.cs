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
        public C_AccountType C_AccountType { get; set; }
        public int C_AccountTypeId { get; set; }
        public string StudentToken { get; set; }
    }
}
