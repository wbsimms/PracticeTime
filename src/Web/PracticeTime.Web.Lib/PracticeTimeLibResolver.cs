using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PracticeTime.Web.Lib.BadgeRules;

namespace PracticeTime.Web.Lib
{
    public class PracticeTimeLibResolver
    {
        private static PracticeTimeLibResolver instance = new PracticeTimeLibResolver();
        private UnityContainer container;
        private PracticeTimeLibResolver()
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
            container.RegisterType<IFirstSessionRule, FirstSessionRule>();
            container.RegisterType<IBadgeRulesEngine, BadgeRulesEngine>();
        }

        public static PracticeTimeLibResolver Instance
        {
            get { return instance; }
        }

        public UnityContainer Container
        {
            get { return this.container; }
        }


    }
}
