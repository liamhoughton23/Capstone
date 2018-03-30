using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAPSTONE.Models;
using CAPSTONE.HelperClasses;

namespace CAPSTONE.Controllers
{
    public class LocationGettersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LocationGetters
        public ActionResult Index()
        {
            return View(db.LocationGetters.ToList());
        }

        // GET: LocationGetters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGetter locationGetter = db.LocationGetters.Find(id);
            if (locationGetter == null)
            {
                return HttpNotFound();
            }
            return View(locationGetter);
        }

        // GET: LocationGetters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationGetters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DestinationID,Location,Destination")] LocationGetter locationGetter)
        {
            if (ModelState.IsValid)
            {
                db.LocationGetters.Add(locationGetter);
                db.SaveChanges();
                AddyLatLong change = new AddyLatLong();
                change.GetLatLng(locationGetter.Location);
                change.GetLatLng(locationGetter.Destination);
                return RedirectToAction("Index");
            }

            return View(locationGetter);
        }

        // GET: LocationGetters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGetter locationGetter = db.LocationGetters.Find(id);
            if (locationGetter == null)
            {
                return HttpNotFound();
            }
            return View(locationGetter);
        }

        // POST: LocationGetters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DestinationID,Location,Destination")] LocationGetter locationGetter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationGetter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locationGetter);
        }

        // GET: LocationGetters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGetter locationGetter = db.LocationGetters.Find(id);
            if (locationGetter == null)
            {
                return HttpNotFound();
            }
            return View(locationGetter);
        }

        // POST: LocationGetters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationGetter locationGetter = db.LocationGetters.Find(id);
            db.LocationGetters.Remove(locationGetter);
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
