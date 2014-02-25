using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace PracticeTime.Web.Test.Controllers
{
    public class TestControllerContext : ControllerContext
    {
        private string userName = "student";
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        public override HttpContextBase HttpContext { get { return new TestHttpContext(){UserName = this.UserName}; }
            set { base.HttpContext = value; }
        }
    }

    public class TestHttpContext : HttpContextBase
    {
        private string userName = "student";
        public string UserName {
            get { return this.userName; }
            set { this.userName = value; }
        }

        public override IPrincipal User
        {
            get
            {
                return new TestPrincipal(UserName);
            }
            set
            {
                base.User = value;
            }
        }
    }

    public class TestPrincipal : IPrincipal
    {
        private string userName = "student";

        public TestPrincipal(string userName)
        {
            this.userName = userName;
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(userName); }
        }
    }
}