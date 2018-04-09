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
    public class PitchStatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public PitchStats stats = new PitchStats();

        // GET: PitchStats
        public ActionResult Index()
        {
            var pitchStats = db.PitchStats.Include(p => p.Player);
            return View(pitchStats.ToList());
        }

        // GET: PitchStats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PitchStats pitchStats = db.PitchStats.Find(id);
            if (pitchStats == null)
            {
                return HttpNotFound();
            }
            return View(pitchStats);
        }

        // GET: PitchStats/Create
        public void Create(int PlayerID, int CoachId, int OpponentOfficialAtBats, int OpponentHits, int EarnedRunsScored, int InningsPitched, int StrikeOuts, int HomeRuns, int Walks, int BattersHBP, int PickOffAttempts, int PickOffs)
        {
            PitchingHelpers helpers = new PitchingHelpers();
            stats.PlayerID = PlayerID;
            stats.CoachID = CoachId;
            stats.EarnedRunAvereage = helpers.ERAcalc(EarnedRunsScored, InningsPitched);
            stats.OpponentBattingAverage = helpers.OpponentBA(OpponentHits, OpponentOfficialAtBats);
            stats.WHIP = helpers.WHIP(Walks, OpponentHits, InningsPitched);
            stats.StrikeOuts = StrikeOuts;
            stats.StrikeOutPercentage = helpers.SimpleDivision(StrikeOuts, OpponentOfficialAtBats);
            stats.PickOffPercentage = helpers.SimpleDivision(PickOffs, PickOffAttempts);
            stats.WalksPerAtBat = helpers.SimpleDivision(OpponentOfficialAtBats, Walks);
            stats.WalksPerAtBat = helpers.SimpleDivision(Walks, InningsPitched);
            db.PitchStats.Add(stats);
            db.SaveChanges();
        }

        // POST: PitchStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,PlayerID,CoachID,EarnedRunAvereage,OpponentBattingAverage,WHIP,StrikeOuts,StrikeOutPercentage,PickOffPercentage,WalksPerAtBat,WalksPerInning")] PitchStats pitchStats)
        {
            if (ModelState.IsValid)
            {
                db.PitchStats.Add(pitchStats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", pitchStats.PlayerID);
            return View(pitchStats);
        }

        // GET: PitchStats/Edit/5
        public void Edit(int PlayerID, int CoachId, int OpponentOfficialAtBats, int OpponentHits, int EarnedRunsScored, int InningsPitched, int StrikeOuts, int HomeRuns, int Walks, int BattersHBP, int PickOffAttempts, int PickOffs)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //PitchStats pitchStats = db.PitchStats.Find(id);
            //if (pitchStats == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", pitchStats.PlayerID);
            //return View(pitchStats);
            var result = from row in db.PitchStats where row.PlayerID == PlayerID select row;
            var resultToUser = result.FirstOrDefault();
            PitchingHelpers helpers = new PitchingHelpers();
            resultToUser.PlayerID = PlayerID;
            resultToUser.CoachID = CoachId;
            resultToUser.EarnedRunAvereage = helpers.ERAcalc(EarnedRunsScored, InningsPitched);
            resultToUser.OpponentBattingAverage = helpers.OpponentBA(OpponentHits, OpponentOfficialAtBats);
            resultToUser.WHIP = helpers.WHIP(Walks, OpponentHits, InningsPitched);
            resultToUser.StrikeOuts = StrikeOuts;
            resultToUser.StrikeOutPercentage = helpers.SimpleDivision(StrikeOuts, OpponentOfficialAtBats);
            resultToUser.PickOffPercentage = helpers.SimpleDivision(PickOffs, PickOffAttempts);
            resultToUser.WalksPerAtBat = helpers.SimpleDivision(OpponentOfficialAtBats, Walks);
            resultToUser.WalksPerAtBat = helpers.SimpleDivision(Walks, InningsPitched);
            db.Entry(resultToUser).State = EntityState.Modified;
            db.SaveChanges();
        }

        // POST: PitchStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,PlayerID,CoachID,EarnedRunAvereage,OpponentBattingAverage,WHIP,StrikeOuts,StrikeOutPercentage,PickOffPercentage,WalksPerAtBat,WalksPerInning")] PitchStats pitchStats)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pitchStats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", pitchStats.PlayerID);
            return View(pitchStats);
        }

        // GET: PitchStats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PitchStats pitchStats = db.PitchStats.Find(id);
            if (pitchStats == null)
            {
                return HttpNotFound();
            }
            return View(pitchStats);
        }

        // POST: PitchStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PitchStats pitchStats = db.PitchStats.Find(id);
            db.PitchStats.Remove(pitchStats);
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
