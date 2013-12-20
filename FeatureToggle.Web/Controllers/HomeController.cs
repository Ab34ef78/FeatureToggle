using FeatureToggle.Web.NFeature;
using NFeature.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using FeatureToggle.Web.Models;

namespace FeatureToggle.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Feature()
        {
            ViewBag.Message = "Feature";

            var viewModel = new FeatureModel();

            return View(viewModel);
        }
    }
}
