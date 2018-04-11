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
    public class ContactPlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContactPlayers
        public ActionResult Index()
        {
            var contactPlayers = db.ContactPlayers.Include(c => c.Player);
            return View(contactPlayers.ToList());
        }

        // GET: ContactPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPlayer contactPlayer = db.ContactPlayers.Find(id);
            if (contactPlayer == null)
            {
                return HttpNotFound();
            }
            return View(contactPlayer);
        }

        // GET: ContactPlayers/Create
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();
            var userRow = from row in db.Coaches where row.UserId == user select row;
            var first = userRow.FirstOrDefault();
            ViewBag.PlayerID = new SelectList(db.Players.Where(o => o.CoachID == first.CoachID), "PlayerID", "FirstName");
            return View();
        }

        // POST: ContactPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,Message,PlayerID,PhoneNumber")] ContactPlayer contactPlayer)
        {
            if (ModelState.IsValid)
            {

                //string user = User.Identity.GetUserId();
                //var result = from row in db.Coaches where row.UserId == user select row;
                //var rowResult = result.FirstOrDefault();
                //rowResult.CoachID = contactCoach.Coach;
                var playerResult = from row in db.Players where row.PlayerID == contactPlayer.PlayerID select row;
                var coachRowResult = playerResult.FirstOrDefault();
                contactPlayer.PhoneNumber = coachRowResult.PhoneNumber;
                db.ContactPlayers.Add(contactPlayer);
                db.SaveChanges();
                string lastPhone = (from n in db.ContactPlayers orderby n.PhoneNumber descending select n.PhoneNumber).FirstOrDefault();
                string lastMassage = (from n in db.ContactPlayers orderby n.Message descending select n.Message).FirstOrDefault();
                Message message = new Message();
                HelperClasses.Twilio twilio = new HelperClasses.Twilio();
                message.content = lastMassage;
                message.recipient = lastPhone;
                twilio.Send(message, lastPhone);
                db.ContactPlayers.Add(contactPlayer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", contactPlayer.PlayerID);
            return View(contactPlayer);
        }

        // GET: ContactPlayers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPlayer contactPlayer = db.ContactPlayers.Find(id);
            if (contactPlayer == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", contactPlayer.PlayerID);
            return View(contactPlayer);
        }

        // POST: ContactPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,Message,PlayerID,PhoneNumber")] ContactPlayer contactPlayer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactPlayer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", contactPlayer.PlayerID);
            return View(contactPlayer);
        }

        // GET: ContactPlayers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPlayer contactPlayer = db.ContactPlayers.Find(id);
            if (contactPlayer == null)
            {
                return HttpNotFound();
            }
            return View(contactPlayer);
        }

        // POST: ContactPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactPlayer contactPlayer = db.ContactPlayers.Find(id);
            db.ContactPlayers.Remove(contactPlayer);
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
