using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAPSTONE.Models;

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
        public ActionResult Create()
        {
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName");
            return View();
        }

        // POST: GameOffenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Game,PlayerID,PlateAppearances,Singles,Doubles,Triples,HRs,TotalBases,Walks,HBP,Scrifices,OnByFeildersChoice,OnByInterference,DroppedThirdStrike,StolenBases,StolenBaseAttempts,SO,OtherBattingOuts,RBIs,RunsScored")] TotalOffense totalOffense)
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
        public ActionResult Edit(int? id)
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
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", gameOffense.PlayerID);
            return View(gameOffense);
        }

        // POST: GameOffenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Game,PlayerID,PlateAppearances,Singles,Doubles,Triples,HRs,TotalBases,Walks,HBP,Scrifices,OnByFeildersChoice,OnByInterference,DroppedThirdStrike,StolenBases,StolenBaseAttempts,SO,OtherBattingOuts,RBIs,RunsScored")] TotalOffense gameOffense)
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
