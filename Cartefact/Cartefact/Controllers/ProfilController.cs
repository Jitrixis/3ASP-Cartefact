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

        // GET: Profil/Edit
        public ActionResult Edit()
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

        // POST: Profil/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string Name, string DrivingHabits, string DriverExperience, string PasswordOld, string PasswordNew, string PasswordConfirm)
        {
            Person person = db.Persons.Find(HttpContext.User.Identity.Name);
            person.Name = Name;
            person.DrivingHabits = DrivingHabits;
            person.DriverExperience = DriverExperience;

            bool passwordValid = true;
            bool passwordChange = false;
            if(PasswordOld != "" || PasswordNew != "" || PasswordConfirm != "")
            {
                passwordChange = true;
                if (!person.CheckPassword(PasswordOld))
                {
                    passwordValid = false;
                    ModelState.AddModelError("PasswordOld", "Your current password is incorrect");
                }
                if(PasswordNew != PasswordConfirm)
                {
                    passwordValid = false;
                    ModelState.AddModelError("PasswordNew", "Passwords aren't similar");
                }
                if(PasswordNew == "")
                {
                    passwordValid = false;
                    ModelState.AddModelError("PasswordNew", "Password can't be empty");
                }
            }

            if (ModelState.IsValid && passwordValid)
            {
                if (passwordChange)
                {
                    person.Password = PasswordNew;
                }
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }
    }
}