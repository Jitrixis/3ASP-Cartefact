using Cartefact.Classes;
using Cartefact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cartefact.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private Context db = new Context();

        // GET: Profil
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.Name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(HttpContext.User.Identity.Name);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
    }
}