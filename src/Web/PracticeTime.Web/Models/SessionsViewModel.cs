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
        public List<Session> AllSessions { get; set; }

        public SessionsViewModel()
        {
            this.AllSessions = new List<Session>();
        }
    }
}