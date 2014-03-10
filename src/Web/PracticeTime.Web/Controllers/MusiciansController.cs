using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    public class MusiciansController : Controller
    {
        //
        // GET: /Musicians/
        public ActionResult Index()
        {
            MusiciansViewModel model = new MusiciansViewModel();
            return View(model);
        }
	}
}