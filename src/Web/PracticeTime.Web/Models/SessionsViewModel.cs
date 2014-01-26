using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Models
{
    public class SessionsViewModel
    {
        private readonly ISessionRepository sessionRepository;

        public SessionsViewModel(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public List<Session> AllSessions()
        {
            return sessionRepository.GetAll();
        }
    }
}