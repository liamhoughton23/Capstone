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
    public class DefenseStatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public DefenseStats defenseStats = new DefenseStats();
        // GET: DefenseStats
        public ActionResult Index()
        {
            var defense = db.Defense.Include(d => d.Player);
            return View(defense.ToList());
        }

        // GET: DefenseStats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefenseStats defenseStats = db.Defense.Find(id);
            if (defenseStats == null)
            {
                return HttpNotFound();
            }
            return View(defenseStats);
        }

        // GET: DefenseStats/Create
        public void Create(int playerID, int CoachID, int position, int errors, int inningsPlayed, int putOuts, int assists)
        {
            DefenseHelpers helpers = new DefenseHelpers();
            MorphingTables morph = new MorphingTables();
            defenseStats.PlayerID = playerID;
            defenseStats.CoachID = CoachID;
            defenseStats.Position = position;
            defenseStats.Errors = errors;
            defenseStats.IP = inningsPlayed;
            defenseStats.PO = putOuts;
            defenseStats.Assists = assists;
            defenseStats.TC = helpers.TotalChances(assists, putOuts, errors);
            defenseStats.FPCT = helpers.FPCT(putOuts, assists, errors);
            db.Defense.Add(defenseStats);
            db.SaveChanges();
        }

        // POST: DefenseStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,PlayerID,CoachID,Positions,IP,TC,PO,Assists,Errors,FPCT")] DefenseStats defenseStats)
        {
            if (ModelState.IsValid)
            {
                db.Defense.Add(defenseStats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", defenseStats.PlayerID);
            return View(defenseStats);
        }

        // GET: DefenseStats/Edit/5
        public void Edit(int playerID, int CoachId, int position, int errors, int inningsPlayed, int putOuts, int assists)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //DefenseStats defenseStats = db.Defense.Find(id);
            //if (defenseStats == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", defenseStats.PlayerID);
            DefenseHelpers helpers = new DefenseHelpers();
            var result = from row in db.Defense where row.PlayerID == playerID select row;
            var resultToUser = result.FirstOrDefault();
            resultToUser.PlayerID = playerID;
            resultToUser.CoachID = CoachId;
            resultToUser.Position = position;
            resultToUser.Errors = errors;
            resultToUser.Assists = assists;
            resultToUser.IP = inningsPlayed;
            resultToUser.PO = putOuts;
            resultToUser.TC = helpers.TotalChances(assists, putOuts, errors);
            resultToUser.FPCT = helpers.FPCT(putOuts, assists, errors);
            db.Entry(resultToUser).State = EntityState.Modified;
            db.SaveChanges();

        }

        // POST: DefenseStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,PlayerID,CoachID,Positions,Games,IP,TC,PO,Assists,Errors,FPCT,CoachID")] DefenseStats defenseStats)
        {
            if (ModelState.IsValid)
            {

                db.Entry(defenseStats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", defenseStats.PlayerID);
            return View(defenseStats);
        }

        // GET: DefenseStats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefenseStats defenseStats = db.Defense.Find(id);
            if (defenseStats == null)
            {
                return HttpNotFound();
            }
            return View(defenseStats);
        }

        // POST: DefenseStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefenseStats defenseStats = db.Defense.Find(id);
            db.Defense.Remove(defenseStats);
            db.SaveChanges();
            return RedirectToAction("Home", "Coaches");
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
