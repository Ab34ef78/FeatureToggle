using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeatureToggle.Web;

namespace FeatureToggle.Web.Controllers
{
    public class FeatureController : Controller
    {
        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(FormCollection formCollection)
        {
            foreach (var item in formCollection)
            {
                string key = item.ToString();
                string value = formCollection[item.ToString()].ToString();
                global::NFeature.Configuration.FeatureState state;
                if (Enum.TryParse(value, out state))
                {
                    FeatureToggle.Web.MyFeature.FeatureRepo.SetFeatureState(key, state);
                }
            }

           return  RedirectToAction("Feature", "Home");
        }

    }
}
