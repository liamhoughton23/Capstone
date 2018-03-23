﻿using System;
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
    public class OffenseStatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        public ActionResult Create(int playerID)
        {
            //ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName");
            
            return View();
        }

        // POST: OffenseStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,Player,TotalPlateAppearances,OfficialAtBats,TotalHits,BA,SLG,OBP,BOBP,SBP,SOR,RunsCreated")] OffenseStats offenseStats, SubmitOffense subOffense)
        {
            if (ModelState.IsValid)
            {
                db.Offense.Add(offenseStats);

                return RedirectToAction("Index");
            }

            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", offenseStats.Player);

            return View(offenseStats);
        }

        // GET: OffenseStats/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", offenseStats.Player);
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

    }
}
