using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using cobrowse.demo.Helpers;
using cobrowse.demo.Models;
using cobrowse.demo.Modules;
using Microsoft.AspNet.SignalR;

namespace cobrowse.demo.Controllers
{
    public class CobrowseController : Controller
    {
        public ActionResult Agent(bool? enableSuccess)
        {
            var model = new AgentModel
                        {
                            EnableSuccess = enableSuccess
                        };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Agent(AgentModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.PushType.Equals("cache", StringComparison.CurrentCultureIgnoreCase))
                {
                    CobrowseCacheHelper.EnableCobrowse(model.Username);
                    return RedirectToAction("Agent", "Cobrowse", new {enableSuccess = true});
                }
                else
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<CobrowseHub>();
                    context.Clients.Group(model.Username).checkCobrowse("admin", "cobrowse enabled");
                    return RedirectToAction("Agent", "Cobrowse", new { enableSuccess = true });
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Emulate(string pushType, string userName)
        {
            var model = new EmulateModel
                        {
                            PushType = pushType,
                            Username = userName
                        };
            SessionHelper.Set<string>(userName, "username");
            FormsAuthentication.SetAuthCookie(model.Username, false);
            return View(model);
        }

        [HttpGet]
        public JsonResult CheckStatus(string username)
        {
            return this.Json(CobrowseCacheHelper.CheckCobrowse(username), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DisableCobrowse(string username)
        {
            CobrowseCacheHelper.DisableCobrowse(username);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
