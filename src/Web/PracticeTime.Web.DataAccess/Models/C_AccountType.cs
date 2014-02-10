using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTime.Web.DataAccess.Models
{
    public class C_AccountType
    {
        public int C_AccountTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
