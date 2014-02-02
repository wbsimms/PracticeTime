using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Lib;
using Unity.Mvc5;

namespace PracticeTime.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            PracticeTimeWebResolver resolver = PracticeTimeWebResolver.Instance;
            resolver.Register(resolver.Container);
            PracticeTimeLibResolver.Instance.Register(resolver.Container); 
            DependencyResolver.SetResolver(new UnityDependencyResolver(resolver.Container));
        }
    }

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
            container.RegisterType<ISessionRepository, SessionRepository>();
            container.RegisterType<IBadgeAwardRepository, BadgeAwardRepository>();
            container.RegisterType<IBadgeRepository, BadgeRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
        }

        public static PracticeTimeWebResolver Instance {
            get { return instance; }
        }

        public UnityContainer Container { get { return this.container; } }
    }
}