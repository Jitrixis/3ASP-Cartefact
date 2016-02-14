using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cartefact.Classes;
using Cartefact.Models;

namespace Cartefact.Areas.Me.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private Context db = new Context();

        // GET: Me/Cars
        public ActionResult Index()
        {
            return Redirect("~/Me/Profil");
        }

        // GET: Me/Cars/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            if(car.Person.Id != HttpContext.User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Me/Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Me/Cars/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Brand,Model,Color,Description,BuyingDate,Kilometers,Location")] Car car)
        {
            Person person = db.Persons.FirstOrDefault(p => p.Id == HttpContext.User.Identity.Name);
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid().ToString();
                car.Location.Id = Guid.NewGuid().ToString();
                car.Person = person;
                if (Array.Exists(new[] { "Admin", "Trusted" }, e => e.Equals(person.Role.RoleName)))
                {
                    car.Status = db.Statuses.FirstOrDefault(s => s.StatusName == "Open");
                }
                else
                {
                    car.Status = db.Statuses.FirstOrDefault(s => s.StatusName == "Pending");
                }
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Me/Cars/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            if (car.Person.Id != HttpContext.User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Me/Cars/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand,Model,Color,Description,BuyingDate,Kilometers,Location")] Car car)
        {
            Person person = db.Persons.FirstOrDefault(p => p.Id == HttpContext.User.Identity.Name);
            if(person.Cars.Count(c => c.Id == car.Id) == 0){
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Me/Cars/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            if (car.Person.Id != HttpContext.User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Me/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Person person = db.Persons.FirstOrDefault(p => p.Id == HttpContext.User.Identity.Name);
            if (person.Cars.Count(c => c.Id == id) == 0)
            {
                return HttpNotFound();
            }
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
