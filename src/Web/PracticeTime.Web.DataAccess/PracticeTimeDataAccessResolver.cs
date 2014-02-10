﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.DataAccess
{
    public class PracticeTimeDataAccessResolver
    {
        private static PracticeTimeDataAccessResolver instance = new PracticeTimeDataAccessResolver();
        private UnityContainer container;

        private PracticeTimeDataAccessResolver()
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
            container.RegisterType<IInstrumentRepository, InstrumentRepository>();
            container.RegisterType<IAccountTypeRepository, AccountTypeRepository>();
        }

        public static PracticeTimeDataAccessResolver Instance {
            get { return instance; }
        }

        public UnityContainer Container
        {
            get { return this.container; }
        }
    }
}
