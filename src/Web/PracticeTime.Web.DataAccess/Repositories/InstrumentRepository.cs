using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Copiers;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface IInstrumentRepository
    {
        C_Instrument GetById(int badgeId);
        List<C_Instrument> GetAll();
    }

    public class InstrumentRepository : IInstrumentRepository
    {
        public C_Instrument GetById(int instrumentId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Instruments.AsNoTracking().FirstOrDefault(x => x.C_InstrumentId == instrumentId);
            }
        }

        public List<C_Instrument> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Instruments.AsNoTracking().ToList();
            }
        }
    }
}
