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
    public class DefenseStatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        public ActionResult Create()
        {
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName");
            return View();
        }

        // POST: DefenseStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,PlayerID,Positions,Games,IP,TC,PO,Assists,Errors,DoublePlays,FPCT")] DefenseStats defenseStats)
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
        public ActionResult Edit(int? id)
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
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", defenseStats.PlayerID);
            return View(defenseStats);
        }

        // POST: DefenseStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,PlayerID,Positions,Games,IP,TC,PO,Assists,Errors,DoublePlays,FPCT")] DefenseStats defenseStats)
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
