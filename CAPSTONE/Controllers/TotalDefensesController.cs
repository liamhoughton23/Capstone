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
    public class TotalDefensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TotalDefenses
        public ActionResult Index()
        {
            var totalDefenses = db.TotalDefenses.Include(t => t.Player);
            return View(totalDefenses.ToList());
        }

        // GET: TotalDefenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TotalDefense totalDefense = db.TotalDefenses.Find(id);
            if (totalDefense == null)
            {
                return HttpNotFound();
            }
            return View(totalDefense);
        }

        // GET: TotalDefenses/Create
        public void Create(int playerID, int CoachId, int position, int errors, int inningsPlayed, int putOuts, int assists, TotalDefense totalDefense)
        {
            totalDefense.PlayerID = playerID;
            totalDefense.CoachID = CoachId;
            totalDefense.Positions = position;
            totalDefense.Errors = errors;
            totalDefense.InningsPlayed = inningsPlayed;
            totalDefense.PutOuts = putOuts;
            totalDefense.Assists = assists;
            db.TotalDefenses.Add(totalDefense);
            db.SaveChanges();
            DefenseStatsController newStats = new DefenseStatsController();
            newStats.Create(totalDefense.PlayerID, totalDefense.CoachID, totalDefense.Positions, totalDefense.Errors, totalDefense.InningsPlayed, totalDefense.PutOuts, totalDefense.Assists);
        }

        // POST: TotalDefenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,PlayerID,CoachID,Positions,Attempts,Errors,InningsPlayed,PutOuts,Assists")] TotalDefense totalDefense)
        {
            if (ModelState.IsValid)
            {
                db.TotalDefenses.Add(totalDefense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", totalDefense.PlayerID);
            return View(totalDefense);
        }

        // GET: TotalDefenses/Edit/5
        public void Edit(int playerID, int CoachID, int position, int errors, int inningsPlayed, int putOuts, int assists)
        {
            MorphingTables morph = new MorphingTables();
            var result = from row in db.TotalDefenses where row.PlayerID == playerID select row;
            var resultToUser = result.FirstOrDefault();
            resultToUser.PlayerID = playerID;
            resultToUser.CoachID = CoachID;
            resultToUser.Positions = position;
            resultToUser.Errors = morph.AddingStats(resultToUser.Errors, errors);
            resultToUser.InningsPlayed = morph.AddingStats(resultToUser.InningsPlayed, inningsPlayed);
            resultToUser.PutOuts = morph.AddingStats(resultToUser.PutOuts, putOuts);
            resultToUser.Assists = morph.AddingStats(resultToUser.Assists, assists);
            db.Entry(resultToUser).State = EntityState.Modified;
            db.SaveChanges();
            DefenseStatsController newStats = new DefenseStatsController();
            newStats.Edit(resultToUser.PlayerID, resultToUser.CoachID, resultToUser.Positions, resultToUser.Errors, resultToUser.InningsPlayed, resultToUser.PutOuts, resultToUser.Assists);
        }

        // POST: TotalDefenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,PlayerID,CoachID,Positions,Errors,InningsPlayed,PutOuts,Assists")] TotalDefense totalDefense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(totalDefense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", totalDefense.PlayerID);
            return View(totalDefense);
        }

        // GET: TotalDefenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TotalDefense totalDefense = db.TotalDefenses.Find(id);
            if (totalDefense == null)
            {
                return HttpNotFound();
            }
            return View(totalDefense);
        }

        // POST: TotalDefenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TotalDefense totalDefense = db.TotalDefenses.Find(id);
            db.TotalDefenses.Remove(totalDefense);
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
