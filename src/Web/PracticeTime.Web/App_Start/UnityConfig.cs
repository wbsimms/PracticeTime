using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PracticeTime.Web.DataAccess.Repositories;
using Unity.Mvc5;

namespace PracticeTime.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            PracticeTimeWebResolver resolver = PracticeTimeWebResolver.Instance;
            DependencyResolver.SetResolver(new UnityDependencyResolver(resolver.Container));
        }
    }
}