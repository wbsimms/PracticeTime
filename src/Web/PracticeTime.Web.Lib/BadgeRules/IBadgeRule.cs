using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Lib.BadgeRules
{
    public interface IBadgeRule
    {
        void Rule(Session session, ResponseModel response);
    }
}
