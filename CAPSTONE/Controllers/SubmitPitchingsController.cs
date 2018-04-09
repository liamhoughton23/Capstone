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
using Microsoft.AspNet.Identity;

namespace CAPSTONE.Controllers
{
    public class SubmitPitchingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubmitPitchings
        public ActionResult Index()
        {
            var submitPitchings = db.SubmitPitchings.Include(s => s.PlayerID);
            return View(submitPitchings.ToList());
        }

        // GET: SubmitPitchings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            if (submitPitching == null)
            {
                return HttpNotFound();
            }
            return View(submitPitching);
        }

        // GET: SubmitPitchings/Create
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();
            var userRow = from row in db.Coaches where row.UserId == user select row;
            var first = userRow.FirstOrDefault();
            ViewBag.PlayerID = new SelectList(db.Players.Where(o => o.CoachID == first.CoachID), "PlayerID", "FirstName");
            return View();
        }

        // POST: SubmitPitchings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,PlayerID,CoachID,OpponentOfficialAtBats,OpponentHits,EarnedRunsScored,InningsPitched,StrikeOuts,HomeRuns,Walks,BattersHBP,PickOffAttempts,PickOffs")] SubmitPitching submitPitching, TotalPitching stats)
        {        
            if (ModelState.IsValid)
           {
                TotalPitchingsController total = new TotalPitchingsController();
                MorphingTables morph = new MorphingTables();
                PitchStatsController off = new PitchStatsController();
                string user = User.Identity.GetUserId();
                var coachRow = from row in db.Coaches where row.UserId == user select row;
                var coachRowResult = coachRow.FirstOrDefault();
                submitPitching.CoachID = coachRowResult.CoachID;
                db.SubmitPitchings.Add(submitPitching);
                db.SaveChanges();
                foreach (var item in db.TotalPitchings)
                {
                    if (item.PlayerID == submitPitching.PlayerID)
                    {
                        total.Edit(submitPitching.PlayerID, submitPitching.CoachID, submitPitching.OpponentOfficialAtBats, submitPitching.OpponentHits, submitPitching.EarnedRunsScored, submitPitching.InningsPitched, submitPitching.StrikeOuts, submitPitching.HomeRuns, submitPitching.Walks, submitPitching.BattersHBP, submitPitching.PickOffAttempts, submitPitching.PickOffs);
                        return RedirectToAction("Home", "Coaches");
                    }
                }

                total.Create(submitPitching.PlayerID, submitPitching.CoachID, submitPitching.OpponentOfficialAtBats, submitPitching.OpponentHits, submitPitching.EarnedRunsScored, submitPitching.InningsPitched, submitPitching.StrikeOuts, submitPitching.HomeRuns, submitPitching.Walks, submitPitching.BattersHBP, submitPitching.PickOffAttempts, submitPitching.PickOffs, stats);
                return RedirectToAction("Home", "Coaches");
           }
           return RedirectToAction("Home", "Coaches");
        }

        // GET: SubmitPitchings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            if (submitPitching == null)
            {
                return HttpNotFound();
            }
            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", submitPitching.Player);
            return View(submitPitching);
        }

        // POST: SubmitPitchings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,Player,OpponentOfficialAtBats,OpponentHits,EarnedRunsScored,InningsPitched,StrikeOuts,HomeRuns,Walks,BattersHBP,PickOffAttempts,PickOffs")] SubmitPitching submitPitching)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submitPitching).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", submitPitching.Player);
            return View(submitPitching);
        }

        // GET: SubmitPitchings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            if (submitPitching == null)
            {
                return HttpNotFound();
            }
            return View(submitPitching);
        }

        // POST: SubmitPitchings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            db.SubmitPitchings.Remove(submitPitching);
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

        public static implicit operator SubmitPitchingsController(SubmitPitching v)
        {
            throw new NotImplementedException();
        }
    }
}
