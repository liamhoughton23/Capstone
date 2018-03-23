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
    public class SubmitOffensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubmitOffenses
        public ActionResult Index()
        {
            var submitOffenses = db.SubmitOffenses.Include(s => s.Player);
            return View(submitOffenses.ToList());
        }

        // GET: SubmitOffenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitOffense submitOffense = db.SubmitOffenses.Find(id);
            if (submitOffense == null)
            {
                return HttpNotFound();
            }
            return View(submitOffense);
        }

        // GET: SubmitOffenses/Create
        public ActionResult Create()
        {
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName");
            return View();
        }

        // POST: SubmitOffenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Game,PlayerID,PlateAppearances,Singles,Doubles,Triples,HRs,Walks,HBP,Scrifices,OnByFeildersChoice,OnByInterference,DroppedThirdStrike,StolenBases,StolenBaseAttempts,SO,OtherBattingOuts,RBIs,RunsScored")] SubmitOffense subOffense)
        {
            if (ModelState.IsValid)
            {
                db.SubmitOffenses.Add(subOffense);
                OffenseStats offenseStats = new OffenseStats();
                offenseStats.Player = subOffense.Player;
                OffenseHelpers helpers = new OffenseHelpers();
                offenseStats.TotalPlateAppearances = helpers.Appearances(subOffense.PlateAppearances);
                offenseStats.OfficialAtBats = helpers.OfficialAtBatsCalculator(subOffense.PlateAppearances, subOffense.Walks, subOffense.HBP, subOffense.Scrifices);
                offenseStats.TotalHits = helpers.TotalHitsCalulator(subOffense.Singles, subOffense.Doubles, subOffense.Triples, subOffense.HRs);
                //offenseStats.TotalBases = helpers.TotalBasesCalc(subOffense.Singles, subOffense.Doubles, subOffense.Triples, subOffense.HRs);
                db.SaveChanges();
                offenseStats.BA = helpers.BattingAverageCalculator(offenseStats.TotalHits, offenseStats.OfficialAtBats);
                offenseStats.SLG = helpers.SluggingPercengateCalculator(subOffense.TotalBases, offenseStats.OfficialAtBats);
                offenseStats.OBP = helpers.OnBasePercentageCalculator(offenseStats.TotalHits, subOffense.Walks, subOffense.HBP, subOffense.OnByInterference, subOffense.DroppedThirdStrike, subOffense.OnByFeildersChoice, offenseStats.OfficialAtBats, subOffense.Scrifices);
                offenseStats.BOBP = helpers.BaseOnBallsPercentage(subOffense.Walks, offenseStats.TotalPlateAppearances);
                offenseStats.SBP = helpers.StolenBasePercentage(subOffense.StolenBases, subOffense.StolenBases);
                offenseStats.SOR = helpers.StrikeOutPercentage(offenseStats.OfficialAtBats, subOffense.SO);
                offenseStats.RunsCreated = helpers.RunsCreatedCalcuator(offenseStats.TotalHits, subOffense.Walks, subOffense.TotalBases, offenseStats.OfficialAtBats);
                db.SaveChanges();
                return RedirectToAction("Index", "OffenseStats");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", subOffense.PlayerID);
            return View(subOffense);
        }

        // GET: SubmitOffenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitOffense submitOffense = db.SubmitOffenses.Find(id);
            if (submitOffense == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", submitOffense.PlayerID);
            return View(submitOffense);
        }

        // POST: SubmitOffenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Game,PlayerID,PlateAppearances,Singles,Doubles,Triples,HRs,Walks,HBP,Scrifices,OnByFeildersChoice,OnByInterference,DroppedThirdStrike,StolenBases,StolenBaseAttempts,SO,OtherBattingOuts,RBIs,RunsScored")] SubmitOffense submitOffense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submitOffense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", submitOffense.PlayerID);
            return View(submitOffense);
        }

        // GET: SubmitOffenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitOffense submitOffense = db.SubmitOffenses.Find(id);
            if (submitOffense == null)
            {
                return HttpNotFound();
            }
            return View(submitOffense);
        }

        // POST: SubmitOffenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubmitOffense submitOffense = db.SubmitOffenses.Find(id);
            db.SubmitOffenses.Remove(submitOffense);
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
