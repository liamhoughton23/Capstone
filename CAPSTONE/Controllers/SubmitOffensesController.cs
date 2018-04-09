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
            string user = User.Identity.GetUserId();
            var userRow = from row in db.Coaches where row.UserId == user select row;
            var first = userRow.FirstOrDefault();
            ViewBag.PlayerID = new SelectList(db.Players.Where(o => o.CoachID == first.CoachID), "PlayerID", "FirstName");
            return View();
        }

        // POST: SubmitOffenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Game,PlayerID,CoachID,PlateAppearances,Singles,Doubles,Triples,HRs,Walks,HBP,Scrifices,OnByFeildersChoice,TotalBases,OnByInterference,DroppedThirdStrike,StolenBases,StolenBaseAttempts,SO,OtherBattingOuts,RBIs,RunsScored")] SubmitOffense subOffense, OffenseStats offenseStats, TotalOffense totalOff )
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.GetUserId();
                var coachRow = from row in db.Coaches where row.UserId == user select row;
                var coachRowResult = coachRow.FirstOrDefault();
                subOffense.CoachID = coachRowResult.CoachID;
                TotalOffensesController total = new TotalOffensesController();
                MorphingTables morph = new MorphingTables();
                OffenseStatsController off = new OffenseStatsController();
                db.SubmitOffenses.Add(subOffense);
                db.SaveChanges();
                foreach (var item in db.GameOffenses)
                {
                    if(item.PlayerID == subOffense.PlayerID)
                    {
                        total.Edit(subOffense.PlayerID, subOffense.CoachID, subOffense.PlateAppearances, subOffense.Singles, subOffense.Doubles, subOffense.Triples, subOffense.HRs, subOffense.Walks, subOffense.HBP, subOffense.Scrifices, subOffense.OnByFeildersChoice, subOffense.TotalBases, subOffense.OnByInterference, subOffense.DroppedThirdStrike, subOffense.StolenBases, subOffense.StolenBaseAttempts, subOffense.SO, subOffense.OtherBattingOuts, subOffense.RBIs, subOffense.RunsScored);
                        return RedirectToAction("Home", "Coaches");
                    }                  
                }

                total.Create(subOffense.PlayerID, subOffense.CoachID, subOffense.PlateAppearances, subOffense.Singles, subOffense.Doubles, subOffense.Triples, subOffense.HRs, subOffense.Walks, subOffense.HBP, subOffense.Scrifices, subOffense.OnByFeildersChoice, subOffense.TotalBases, subOffense.OnByInterference, subOffense.DroppedThirdStrike, subOffense.StolenBases, subOffense.StolenBaseAttempts, subOffense.SO, subOffense.OtherBattingOuts, subOffense.RBIs, subOffense.RunsScored, totalOff);
                off.Create(totalOff.PlayerID, totalOff.CoachID, totalOff.PlateAppearances, totalOff.Singles, totalOff.Doubles, totalOff.Triples, totalOff.HRs, totalOff.Walks, totalOff.HBP, totalOff.Scrifices, totalOff.OnByFeildersChoice, totalOff.TotalBases, totalOff.OnByInterference, totalOff.DroppedThirdStrike, totalOff.StolenBases, totalOff.StolenBaseAttempts, totalOff.SO, totalOff.OtherBattingOuts, totalOff.RBIs, totalOff.RunsScored);      
                return RedirectToAction("Home", "Coaches");
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

        public void Pitching()
        {
            
        }


    }
}
