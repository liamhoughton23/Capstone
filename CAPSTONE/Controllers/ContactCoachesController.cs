using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAPSTONE.Models;
using Microsoft.AspNet.Identity;
using CAPSTONE.HelperClasses;

namespace CAPSTONE.Controllers
{
    public class ContactCoachesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContactCoaches
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContactCoaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactCoach contactCoach = db.ContactCoaches.Find(id);
            if (contactCoach == null)
            {
                return HttpNotFound();
            }
            return View(contactCoach);
        }

        // GET: ContactCoaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactCoaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,Message,Coach,PhoneNumber")] ContactCoach contactCoach)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.GetUserId();
                var result = from row in db.Players where row.UserId == user select row;
                var rowResult = result.FirstOrDefault();
                //rowResult.CoachID = contactCoach.Coach;
                var coachResult = from row in db.Coaches where row.CoachID == rowResult.CoachID select row;
                var coachRowResult = coachResult.FirstOrDefault();
                contactCoach.PhoneNumber = coachRowResult.PhoneNumber;
                db.ContactCoaches.Add(contactCoach);
                db.SaveChanges();
                string lastPhone = (from n in db.ContactCoaches orderby n.PhoneNumber descending select n.PhoneNumber).FirstOrDefault();
                string lastMassage = (from n in db.ContactCoaches orderby n.Message descending select n.Message).FirstOrDefault();
                Message message = new Message();
                HelperClasses.Twilio twilio = new HelperClasses.Twilio();
                message.content = lastMassage;
                message.recipient = lastPhone;
                twilio.Send(message, lastPhone);
                return RedirectToAction("Index");
            }

         
            return View(contactCoach);
        }

        // GET: ContactCoaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactCoach contactCoach = db.ContactCoaches.Find(id);
            if (contactCoach == null)
            {
                return HttpNotFound();
            }
            return View(contactCoach);
        }

        // POST: ContactCoaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,Message,Coach,PhoneNumber")] ContactCoach contactCoach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactCoach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactCoach);
        }

        // GET: ContactCoaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactCoach contactCoach = db.ContactCoaches.Find(id);
            if (contactCoach == null)
            {
                return HttpNotFound();
            }
            return View(contactCoach);
        }

        // POST: ContactCoaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactCoach contactCoach = db.ContactCoaches.Find(id);
            db.ContactCoaches.Remove(contactCoach);
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
