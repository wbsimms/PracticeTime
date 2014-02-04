using Microsoft.Practices.Unity;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess;
using PracticeTime.Web.Lib;

namespace PracticeTime.Web
{
    public class PracticeTimeWebResolver
    {
        private static PracticeTimeWebResolver instance = new PracticeTimeWebResolver();
        private UnityContainer container;

        private PracticeTimeWebResolver()
        {
            Init();
        }

        private void Init()
        {
            container = new UnityContainer();
            Register(container);
        }

        public void Register(UnityContainer container)
        {
            PracticeTimeLibResolver.Instance.Register(container);
            PracticeTimeDataAccessResolver.Instance.Register(container);
            container.RegisterType<AccountController>(new InjectionConstructor());
        }

        public static PracticeTimeWebResolver Instance {
            get { return instance; }
        }

        public UnityContainer Container { get { return this.container; } }
    }
}