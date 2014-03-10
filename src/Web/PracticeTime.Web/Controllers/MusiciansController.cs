using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    public class MusiciansController : Controller
    {
        private IApplicationUserRepository applicationUserRepository;

        public MusiciansController(IApplicationUserRepository applicationUser)
        {
            this.applicationUserRepository = applicationUser;
        }

        //
        // GET: /Musicians/
        public ActionResult Index()
        {
            MusiciansViewModel model = new MusiciansViewModel();
            model.PublicUsers = applicationUserRepository.GetAppPublicProfiles();
            return View(model);
        }
	}
}