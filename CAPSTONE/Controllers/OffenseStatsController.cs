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
    public class OffenseStatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public OffenseStats offenseStats = new OffenseStats();

        // GET: OffenseStats
        public ActionResult Index()
        {
            var offense = db.Offense.Include(o => o.Player);
            return View(offense.ToList());
        }

        // GET: OffenseStats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffenseStats offenseStats = db.Offense.Find(id);
            if (offenseStats == null)
            {
                return HttpNotFound();
            }
            return View(offenseStats);
        }

        // GET: OffenseStats/Create
        public ActionResult Create(int PlayerID, int CoachID, int PlateAppearances, int Singles, int Doubles, int Triples, int HRs, int Walks, int HBP, int Scrifices, int OnByFeildersChoice, int TotalBases, int OnByInterference, int DroppedThirdStrike, int StolenBases, int StolenBaseAttempts, int SO, int OtherBattingOuts, int RBIs, int RunsScored)
        {
            OffenseHelpers helpers = new OffenseHelpers();
            //ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName");
            offenseStats.PlayerID = PlayerID;
            offenseStats.CoachID = CoachID;
            offenseStats.TotalPlateAppearances = helpers.Appearances(PlateAppearances);
            offenseStats.OfficialAtBats = helpers.OfficialAtBatsCalculator(PlateAppearances, Walks, HBP, Scrifices);
            offenseStats.TotalHits = helpers.TotalHitsCalulator(Singles, Doubles, Triples, HRs);
            offenseStats.TotalBases = helpers.TotalBasesCalc(Singles, Doubles, Triples, HRs);
            db.SaveChanges();
            offenseStats.BA = helpers.BattingAverageCalculator(offenseStats.TotalHits, offenseStats.OfficialAtBats);
            offenseStats.SLG = helpers.SluggingPercengateCalculator(TotalBases, offenseStats.OfficialAtBats);
            offenseStats.OBP = helpers.OnBasePercentageCalculator(offenseStats.TotalHits, Walks, HBP, OnByInterference, DroppedThirdStrike, OnByFeildersChoice, offenseStats.OfficialAtBats, Scrifices);
            offenseStats.BOBP = helpers.BaseOnBallsPercentage(Walks, offenseStats.TotalPlateAppearances);
            offenseStats.SBP = helpers.StolenBasePercentage(StolenBases, StolenBases);
            offenseStats.SOR = helpers.StrikeOutPercentage(offenseStats.OfficialAtBats, SO);
            offenseStats.RunsCreated = helpers.RunsCreatedCalcuator(offenseStats.TotalHits, Walks,TotalBases, offenseStats.OfficialAtBats);
            db.Offense.Add(offenseStats);
            db.SaveChanges();
            return View();
        }

        // POST: OffenseStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,Player,TotalPlateAppearances,OfficialAtBats,TotalHits,TotalBases,BA,SLG,OBP,BOBP,SBP,SOR,RunsCreated")] OffenseStats offenseStats)
        {
            

            if (ModelState.IsValid)
            {
                db.Offense.Add(offenseStats);

                db.SaveChanges();


                return RedirectToAction("Index");
            }

            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", offenseStats.Player);

            return View(offenseStats);
        }

        // GET: OffenseStats/Edit/5
        public ActionResult Edit(int PlayerID, int CoachID, int PlateAppearances, int Singles, int Doubles, int Triples, int HRs, int Walks, int HBP, int Scrifices, int OnByFeildersChoice, int TotalBases, int OnByInterference, int DroppedThirdStrike, int StolenBases, int StolenBaseAttempts, int SO, int OtherBattingOuts, int RBIs, int RunsScored)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OffenseStats offenseStats = db.Offense.Find(id);
            //if (offenseStats == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", offenseStats.Player);
            OffenseHelpers helpers = new OffenseHelpers();
            var result = from row in db.Offense where row.PlayerID == PlayerID select row;
            var resultToUser = result.FirstOrDefault();
            resultToUser.PlayerID = PlayerID;
            resultToUser.CoachID = CoachID;
            resultToUser.TotalPlateAppearances = helpers.Appearances(PlateAppearances);
            resultToUser.OfficialAtBats = helpers.OfficialAtBatsCalculator(PlateAppearances, Walks, HBP, Scrifices);
            resultToUser.TotalHits = helpers.TotalHitsCalulator(Singles, Doubles, Triples, HRs);
            resultToUser.TotalBases = helpers.TotalBasesCalc(Singles, Doubles, Triples, HRs);
            db.SaveChanges();
            resultToUser.BA = helpers.BattingAverageCalculator(resultToUser.TotalHits, resultToUser.OfficialAtBats);
            resultToUser.SLG = helpers.SluggingPercengateCalculator(TotalBases, resultToUser.OfficialAtBats);
            resultToUser.OBP = helpers.OnBasePercentageCalculator(resultToUser.TotalHits, Walks, HBP, OnByInterference, DroppedThirdStrike, OnByFeildersChoice, resultToUser.OfficialAtBats, Scrifices);
            resultToUser.BOBP = helpers.BaseOnBallsPercentage(Walks, resultToUser.TotalPlateAppearances);
            resultToUser.SBP = helpers.StolenBasePercentage(StolenBases, StolenBases);
            resultToUser.SOR = helpers.StrikeOutPercentage(resultToUser.OfficialAtBats, SO);
            resultToUser.RunsCreated = helpers.RunsCreatedCalcuator(resultToUser.TotalHits, Walks, TotalBases, resultToUser.OfficialAtBats);
            db.Entry(resultToUser).State = EntityState.Modified;
            db.SaveChanges();
            return View(offenseStats);
        }

        // POST: OffenseStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,Player,TotalPlateAppearances,OfficialAtBats,TotalHits,BA,SLG,TotalBases,OBP,BOBP,SBP,HRR,SOR,OCR,RunsCreated")] OffenseStats offenseStats)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offenseStats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", offenseStats.Player);
            return View(offenseStats);
        }

        // GET: OffenseStats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffenseStats offenseStats = db.Offense.Find(id);
            if (offenseStats == null)
            {
                return HttpNotFound();
            }
            return View(offenseStats);
        }

        // POST: OffenseStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OffenseStats offenseStats = db.Offense.Find(id);
            db.Offense.Remove(offenseStats);
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
        //offenseStats.Player = subOffense.Player;
        //    offenseStats.TotalPlateAppearances = helpers.Appearances(subOffense.PlateAppearances);
        //    offenseStats.OfficialAtBats = helpers.OfficialAtBatsCalculator(subOffense.PlateAppearances, subOffense.Walks, subOffense.HBP, subOffense.Scrifices);
        //    offenseStats.TotalHits = helpers.TotalHitsCalulator(subOffense.Singles, subOffense.Doubles, subOffense.Triples, subOffense.HRs);
        //    offenseStats.TotalBases = helpers.TotalBasesCalc(subOffense.Singles, subOffense.Doubles, subOffense.Triples, subOffense.HRs);
        //    db.SaveChanges();
        //    offenseStats.BA = helpers.BattingAverageCalculator(offenseStats.TotalHits, offenseStats.OfficialAtBats);
        //    offenseStats.SLG = helpers.SluggingPercengateCalculator(subOffense.TotalBases, offenseStats.OfficialAtBats);
        //    offenseStats.OBP = helpers.OnBasePercentageCalculator(offenseStats.TotalHits, subOffense.Walks, subOffense.HBP, subOffense.OnByInterference, subOffense.DroppedThirdStrike, subOffense.OnByFeildersChoice, offenseStats.OfficialAtBats, subOffense.Scrifices);
        //    offenseStats.BOBP = helpers.BaseOnBallsPercentage(subOffense.Walks, offenseStats.TotalPlateAppearances);
        //    offenseStats.SBP = helpers.StolenBasePercentage(subOffense.StolenBases, subOffense.StolenBases);
        //    offenseStats.SOR = helpers.StrikeOutPercentage(offenseStats.OfficialAtBats, subOffense.SO);
        //    offenseStats.RunsCreated = helpers.RunsCreatedCalcuator(offenseStats.TotalHits, subOffense.Walks, subOffense.TotalBases, offenseStats.OfficialAtBats);

    }
}
