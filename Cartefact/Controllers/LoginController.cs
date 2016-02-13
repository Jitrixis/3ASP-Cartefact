using Cartefact.Classes;
using Cartefact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cartefact.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private Context db = new Context();

        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string nickname, string password, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Person user = db.Persons.FirstOrDefault(p => p.Nickname == nickname);
                if (user != null)
                {
                    if (user.CheckPassword(password))
                    {
                        FormsAuthentication.SetAuthCookie(user.Id, false);
                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        return Redirect("/");
                    }
                }
                ModelState.AddModelError("nickname", "Nickname and/or passwords are incorrects");
            }
            return View();
        }
    }
}