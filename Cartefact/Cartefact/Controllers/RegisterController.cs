using Cartefact.Classes;
using Cartefact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cartefact.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private Context db = new Context();

        // GET: Register
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
        public ActionResult Index([Bind(Include = "Name,Nickname,DrivingHabits,DriverExperience")] Person person, string Password, string PasswordConfirm)
        {
            if (ModelState.IsValid && Password == PasswordConfirm && Password != "")
            {
                person.Id = Guid.NewGuid().ToString();
                person.Password = Password;
                person.Role = db.Roles.FirstOrDefault(r => r.RoleName == "User");
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            ModelState.AddModelError("Password", "Passwords aren't similar");
            PasswordConfirm = "";

            return View(person);
        }
    }
}