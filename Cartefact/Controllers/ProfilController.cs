using Cartefact.Classes;
using Cartefact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cartefact.Controllers
{
    [AllowAnonymous]
    public class ProfilController : Controller
    {
        private Context db = new Context();

        // GET: Profil/Index/id
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.FirstOrDefault(p => p.Nickname == id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
    }
}