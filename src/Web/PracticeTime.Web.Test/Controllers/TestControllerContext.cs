using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace PracticeTime.Web.Test.Controllers
{
    public class TestControllerContext : ControllerContext
    {
        public override HttpContextBase HttpContext { get { return new TestHttpContext(); }
            set { base.HttpContext = value; }
        }
    }

    public class TestHttpContext : HttpContextBase
    {
        public override IPrincipal User { get { return new TestPrincipal(); } set { base.User = value; } }
    }

    public class TestPrincipal : IPrincipal
    {
        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity("genericIdentidy"); }
        }
    }
}