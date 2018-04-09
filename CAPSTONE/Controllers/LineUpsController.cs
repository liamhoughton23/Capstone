using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Threading.Tasks;
using CAPSTONE.Models;
using CAPSTONE.HelperClasses;
using Microsoft.AspNet.Identity;

namespace CAPSTONE.Controllers
{
    public class LineUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LineUps
        public ActionResult Index()
        {
            List<LineUp> lineUp = new List<LineUp>();
            string user = User.Identity.GetUserId();
            var coachRow = from row in db.Coaches where row.UserId == user select row;
            var coachRowResult = coachRow.FirstOrDefault();
            int coachID = coachRowResult.CoachID;
            var playerRow = from row in db.Players where row.UserId == user select row;
            var playerRowResult = coachRow.FirstOrDefault();
            int playerCoachID = playerRowResult.CoachID;
            foreach (var item in db.Lineups)
            {
                if (item.CoachID == coachID || item.CoachID == playerCoachID)
                {
                    lineUp.Add(item);
                }
            }
            return View(lineUp);
        }

        // GET: LineUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.Lineups.Find(id);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            return View(lineUp);
        }

        // GET: LineUps/Create
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();
            var userRow = from row in db.Coaches where row.UserId == user select row;
            var first = userRow.FirstOrDefault();
            ViewBag.PlayerID = new SelectList(db.Players.Where(o=>o.CoachID == first.CoachID), "PlayerID", "FirstName");        
            return View();
        }

        // POST: LineUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Position,PlayerID,CoachID")] LineUp lineUp)
        {
            //if (ModelState.IsValid)
            //{
                string sameUser = User.Identity.GetUserId();
                var result = from row in db.Coaches where row.UserId == sameUser select row;
                var resultToUser = result.FirstOrDefault();
                lineUp.CoachID = resultToUser.CoachID;
                db.Lineups.Add(lineUp);
                db.SaveChanges();
                return RedirectToAction("Index");
            //}

            //ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", lineUp.PlayerID);
            //return View(lineUp);
        }

        // GET: LineUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.Lineups.Find(id);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", lineUp.PlayerID);
            return View(lineUp);
        }

        // POST: LineUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Position,PlayerID")] LineUp lineUp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", lineUp.PlayerID);
            return View(lineUp);
        }

        // GET: LineUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.Lineups.Find(id);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            return View(lineUp);
        }

        // POST: LineUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineUp lineUp = db.Lineups.Find(id);
            db.Lineups.Remove(lineUp);
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

        public ActionResult SentText()
        {
            Message message = new Message();
            string user = User.Identity.GetUserId();
            var coachRow = from row in db.Coaches where row.UserId == user select row;
            var coachRowResult = coachRow.FirstOrDefault();
            foreach (var item in db.Players)
            {
                if(item.CoachID == coachRowResult.CoachID)
                {
                    message.content = "The lineups have been sent! Check if your in it now!";
                    message.recipient = item.PhoneNumber;
                    HelperClasses.Twilio twilio = new HelperClasses.Twilio();
                    twilio.Send(message, item.PhoneNumber);
                    
                }
            }
            return View();
        }
    }
}
