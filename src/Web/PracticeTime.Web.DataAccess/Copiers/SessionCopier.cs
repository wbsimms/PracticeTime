using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Copiers
{
    public class SessionCopier
    {
        public Session Copy(Session from)
        {
            if (from == null) return null;
            Session to = new Session();
            to.SessionId = from.SessionId;
            Merge(from,to);
            return to;
        }

        public void Merge(Session from, Session to)
        {
            if (from == null) return;
            to.SessionId = from.SessionId;
            to.Time = from.Time;
            to.Title = from.Title;
            to.User = from.User;
            to.SessionDateTimeUtc = from.SessionDateTimeUtc;
            to.TimeZoneOffset = from.TimeZoneOffset;
            to.UserId = from.UserId;
        }
    }
}
