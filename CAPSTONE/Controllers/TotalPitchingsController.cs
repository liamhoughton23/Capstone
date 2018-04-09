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
    public class TotalPitchingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TotalPitchings
        public ActionResult Index()
        {
            var totalPitchings = db.TotalPitchings.Include(t => t.Player);
            return View(totalPitchings.ToList());
        }

        // GET: TotalPitchings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TotalPitching totalPitching = db.TotalPitchings.Find(id);
            if (totalPitching == null)
            {
                return HttpNotFound();
            }
            return View(totalPitching);
        }

        // GET: TotalPitchings/Create
        public void Create(int PlayerID, int CoachId, int OpponentOfficialAtBats, int OpponentHits, int EarnedRunsScored, int InningsPitched, int StrikeOuts, int HomeRuns, int Walks, int BattersHBP, int PickOffAttempts, int PickOffs, TotalPitching total)
        {
            total.PlayerID = PlayerID;
            total.CoachID = CoachId;
            total.OpponentOfficialAtBats = OpponentOfficialAtBats;
            total.OpponentHits = OpponentHits;
            total.EarnedRunsScored = EarnedRunsScored;
            total.InningsPitched = InningsPitched;
            total.StrikeOuts = StrikeOuts;
            total.HomeRuns = HomeRuns;
            total.Walks = Walks;
            total.BattersHBP = BattersHBP;
            total.PickOffAttempts = PickOffAttempts;
            total.PickOffs = PickOffs;
            db.TotalPitchings.Add(total);
            db.SaveChanges();
            PitchStatsController pitch = new PitchStatsController();
            pitch.Create(total.PlayerID, total.CoachID, total.OpponentOfficialAtBats, total.OpponentHits, total.EarnedRunsScored, total.InningsPitched, total.StrikeOuts, total.HomeRuns, total.Walks, total.BattersHBP, total.PickOffAttempts, total.PickOffAttempts);
        }

        // POST: TotalPitchings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,PlayerID,CoachID,OpponentOfficialAtBats,OpponentHits,EarnedRunsScored,InningsPitched,StrikeOuts,HomeRuns,Walks,BattersHBP,PickOffAttempts,PickOffs")] TotalPitching totalPitching)
        {
            if (ModelState.IsValid)
            {
                db.TotalPitchings.Add(totalPitching);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", totalPitching.PlayerID);
            return View(totalPitching);
        }

        // GET: TotalPitchings/Edit/5
        public void Edit(int PlayerID, int CoachId, int OpponentOfficialAtBats, int OpponentHits, int EarnedRunsScored, int InningsPitched, int StrikeOuts, int HomeRuns, int Walks, int BattersHBP, int PickOffAttempts, int PickOffs)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TotalPitching totalPitching = db.TotalPitchings.Find(id);
            //if (totalPitching == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", totalPitching.PlayerID);
            //return View(totalPitching);
            MorphingTables morph = new MorphingTables();
            var result = from row in db.TotalPitchings where row.PlayerID == PlayerID select row;
            var resultToUser = result.FirstOrDefault();
            resultToUser.PlayerID = PlayerID;
            resultToUser.CoachID = CoachId;
            resultToUser.OpponentOfficialAtBats = morph.AddingStats(OpponentOfficialAtBats, resultToUser.OpponentOfficialAtBats);
            resultToUser.OpponentHits = morph.AddingStats(OpponentHits, resultToUser.OpponentHits);
            resultToUser.EarnedRunsScored = morph.AddingStats(EarnedRunsScored, resultToUser.EarnedRunsScored);
            resultToUser.InningsPitched = morph.AddingStats(InningsPitched, resultToUser.InningsPitched);
            resultToUser.StrikeOuts = morph.AddingStats(StrikeOuts, resultToUser.StrikeOuts);
            resultToUser.HomeRuns = morph.AddingStats(HomeRuns, resultToUser.HomeRuns);
            resultToUser.Walks = morph.AddingStats(Walks, resultToUser.Walks);
            resultToUser.BattersHBP = morph.AddingStats(BattersHBP, resultToUser.BattersHBP);
            resultToUser.PickOffAttempts = morph.AddingStats(PickOffAttempts, resultToUser.PickOffAttempts); 
            resultToUser.PickOffs = morph.AddingStats(PickOffs, resultToUser.PickOffs);
            db.Entry(resultToUser).State = EntityState.Modified;
            db.SaveChanges();
            PitchStatsController pitch = new PitchStatsController();
            pitch.Edit(resultToUser.PlayerID, resultToUser.CoachID, resultToUser.OpponentOfficialAtBats, resultToUser.OpponentHits, resultToUser.EarnedRunsScored, resultToUser.InningsPitched, resultToUser.StrikeOuts, resultToUser.HomeRuns, resultToUser.Walks, resultToUser.BattersHBP, resultToUser.PickOffAttempts, resultToUser.PickOffs);

        }

        // POST: TotalPitchings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,PlayerID,CoachID,OpponentOfficialAtBats,OpponentHits,EarnedRunsScored,InningsPitched,StrikeOuts,HomeRuns,Walks,BattersHBP,PickOffAttempts,PickOffs")] TotalPitching totalPitching)
        {
            if (ModelState.IsValid)
            {
                db.Entry(totalPitching).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", totalPitching.PlayerID);
            return View(totalPitching);
        }

        // GET: TotalPitchings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TotalPitching totalPitching = db.TotalPitchings.Find(id);
            if (totalPitching == null)
            {
                return HttpNotFound();
            }
            return View(totalPitching);
        }

        // POST: TotalPitchings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TotalPitching totalPitching = db.TotalPitchings.Find(id);
            db.TotalPitchings.Remove(totalPitching);
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
