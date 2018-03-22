﻿using System;
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
    public class PitchStatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        public ActionResult Create()
        {
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName");
            return View();
        }

        // POST: PitchStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,PlayerID,EarnedRunAvereage,OpponentBattingAverage,WHIP,StrikeOuts,StrikeOutPercentage,PickOffPercentage,HitBatterRatio,WalksPerAtBat,WalksPerInning,HRratio,StrikeOutRatio,StrikeOutPerWalkRatio")] PitchStats pitchStats)
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
        public ActionResult Edit(int? id)
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
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", pitchStats.PlayerID);
            return View(pitchStats);
        }

        // POST: PitchStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,PlayerID,EarnedRunAvereage,OpponentBattingAverage,WHIP,StrikeOuts,StrikeOutPercentage,PickOffPercentage,HitBatterRatio,WalksPerAtBat,WalksPerInning,HRratio,StrikeOutRatio,StrikeOutPerWalkRatio")] PitchStats pitchStats)
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
