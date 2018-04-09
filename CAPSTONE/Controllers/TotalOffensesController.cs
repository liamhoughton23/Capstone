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
    public class TotalOffensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GameOffenses
        public ActionResult Index()
        {
            var gameOffenses = db.GameOffenses.Include(g => g.Player);
            return View(gameOffenses.ToList());
        }

        // GET: GameOffenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TotalOffense gameOffense = db.GameOffenses.Find(id);
            if (gameOffense == null)
            {
                return HttpNotFound();
            }
            return View(gameOffense);
        }

        // GET: GameOffenses/Create
        public void Create(int PlayerID, int CoachID, int PlateAppearances, int Singles, int Doubles, int Triples, int HRs, int Walks, int HBP, int Scrifices, int OnByFeildersChoice, int TotalBases, int OnByInterference, int DroppedThirdStrike, int StolenBases, int StolenBaseAttempts, int SO, int OtherBattingOuts, int RBIs, int RunsScored, TotalOffense stats)
        {
            stats.PlayerID = PlayerID;
            stats.CoachID = CoachID;
            stats.PlateAppearances = PlateAppearances;
            stats.Singles = Singles;
            stats.Doubles = Doubles;
            stats.Triples = Triples;
            stats.HRs = HRs;
            stats.Walks = Walks;
            stats.HBP = HBP;
            stats.Scrifices = Scrifices;
            stats.OnByFeildersChoice = OnByFeildersChoice;
            stats.TotalBases = TotalBases;
            stats.OnByInterference = OnByInterference;
            stats.DroppedThirdStrike = DroppedThirdStrike;
            stats.StolenBases = StolenBases;
            stats.StolenBaseAttempts = StolenBaseAttempts;
            stats.SO = SO;
            stats.OtherBattingOuts = OtherBattingOuts;
            stats.RBIs = RBIs;
            stats.RunsScored = RunsScored;
            db.GameOffenses.Add(stats);
            db.SaveChanges();
        }

        // POST: GameOffenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Game,PlayerID,CoachID,PlateAppearances,Singles,Doubles,Triples,HRs,TotalBases,Walks,HBP,Scrifices,OnByFeildersChoice,OnByInterference,DroppedThirdStrike,StolenBases,StolenBaseAttempts,SO,OtherBattingOuts,RBIs,RunsScored")] TotalOffense totalOffense)
        {
            if (ModelState.IsValid)
            {
                db.GameOffenses.Add(totalOffense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", totalOffense.PlayerID);
            return View(totalOffense);
        }

        // GET: GameOffenses/Edit/5
        public ActionResult Edit(int PlayerID, int CoachID, int PlateAppearances, int Singles, int Doubles, int Triples, int HRs, int Walks, int HBP, int Scrifices, int OnByFeildersChoice, int TotalBases, int OnByInterference, int DroppedThirdStrike, int StolenBases, int StolenBaseAttempts, int SO, int OtherBattingOuts, int RBIs, int RunsScored)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TotalOffense gameOffense = db.GameOffenses.Find(id);
            //if (gameOffense == null)
            //{
            //    return HttpNotFound();
            //}
            MorphingTables morph = new MorphingTables();
            var result = from row in db.GameOffenses where row.PlayerID == PlayerID select row;
            var resultToUser = result.FirstOrDefault();
            resultToUser.CoachID = CoachID;
            resultToUser.PlayerID = PlayerID;
            resultToUser.PlateAppearances = morph.AddingStats(PlateAppearances, resultToUser.PlateAppearances);
            resultToUser.Singles = morph.AddingStats(Singles, resultToUser.Singles);
            resultToUser.Doubles = morph.AddingStats(Doubles, resultToUser.Doubles);
            resultToUser.Triples = morph.AddingStats(Triples, resultToUser.Triples);
            resultToUser.HRs = morph.AddingStats(HRs, resultToUser.HRs);
            resultToUser.Walks = morph.AddingStats(Walks, resultToUser.Walks);
            resultToUser.HBP = morph.AddingStats(HBP, resultToUser.HBP);
            resultToUser.Scrifices = morph.AddingStats(Scrifices, resultToUser.Scrifices);
            resultToUser.OnByFeildersChoice = morph.AddingStats(OnByFeildersChoice, resultToUser.OnByFeildersChoice);
            resultToUser.TotalBases = morph.AddingStats(TotalBases, resultToUser.TotalBases);
            resultToUser.OnByInterference = morph.AddingStats(OnByInterference, resultToUser.OnByInterference);
            resultToUser.DroppedThirdStrike = morph.AddingStats(DroppedThirdStrike, resultToUser.DroppedThirdStrike);
            resultToUser.StolenBases = morph.AddingStats(StolenBases, resultToUser.StolenBases);
            resultToUser.StolenBaseAttempts = morph.AddingStats(StolenBaseAttempts, resultToUser.StolenBaseAttempts);
            resultToUser.SO = morph.AddingStats(SO, resultToUser.SO);
            resultToUser.OtherBattingOuts = morph.AddingStats(OtherBattingOuts, resultToUser.OtherBattingOuts);
            resultToUser.RBIs = morph.AddingStats(RBIs, resultToUser.RBIs);
            resultToUser.RunsScored = morph.AddingStats(RunsScored, resultToUser.RunsScored);
            db.Entry(resultToUser).State = EntityState.Modified;
            db.SaveChanges();
            OffenseStatsController newStats = new OffenseStatsController();
            newStats.Edit(resultToUser.PlayerID, resultToUser.CoachID, resultToUser.PlateAppearances, resultToUser.Singles, resultToUser.Doubles, resultToUser.Triples, resultToUser.HRs, resultToUser.Walks, resultToUser.HBP, resultToUser.Scrifices, resultToUser.OnByFeildersChoice, resultToUser.TotalBases, resultToUser.OnByInterference, resultToUser.DroppedThirdStrike, resultToUser.StolenBases, resultToUser.StolenBaseAttempts, resultToUser.SO, resultToUser.OtherBattingOuts, resultToUser.RBIs, resultToUser.RunsScored);
            return View();
        }

        // POST: GameOffenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Game,PlayerID,CoachID,PlateAppearances,Singles,Doubles,Triples,HRs,TotalBases,Walks,HBP,Scrifices,OnByFeildersChoice,OnByInterference,DroppedThirdStrike,StolenBases,StolenBaseAttempts,SO,OtherBattingOuts,RBIs,RunsScored")] TotalOffense gameOffense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameOffense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", gameOffense.PlayerID);
            return View(gameOffense);
        }

        // GET: GameOffenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TotalOffense gameOffense = db.GameOffenses.Find(id);
            if (gameOffense == null)
            {
                return HttpNotFound();
            }
            return View(gameOffense);
        }

        // POST: GameOffenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TotalOffense gameOffense = db.GameOffenses.Find(id);
            db.GameOffenses.Remove(gameOffense);
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
