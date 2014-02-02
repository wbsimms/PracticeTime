using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Repositories;
using Unity.Mvc5;

namespace PracticeTime.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISessionRepository, SessionRepository>();
            container.RegisterType<IBadgeAwardRepository, BadgeAwardRepository>();
            container.RegisterType<IBadgeRepository, BadgeRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}