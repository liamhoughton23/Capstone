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
    public class SubmitPitchingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubmitPitchings
        public ActionResult Index()
        {
            var submitPitchings = db.SubmitPitchings.Include(s => s.PlayerID);
            return View(submitPitchings.ToList());
        }

        // GET: SubmitPitchings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            if (submitPitching == null)
            {
                return HttpNotFound();
            }
            return View(submitPitching);
        }

        // GET: SubmitPitchings/Create
        public ActionResult Create()
        {
            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName");
            return View();
        }

        // POST: SubmitPitchings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,Player,OpponentOfficialAtBats,OpponentHits,EarnedRunsScored,InningsPitched,StrikeOuts,HomeRuns,Walks,BattersHBP,PickOffAttempts,PickOffs")] SubmitPitching submitPitching)
        {
            if (ModelState.IsValid)
            {
                db.SubmitPitchings.Add(submitPitching);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", submitPitching.Player);
            return View(submitPitching);
        }

        // GET: SubmitPitchings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            if (submitPitching == null)
            {
                return HttpNotFound();
            }
            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", submitPitching.Player);
            return View(submitPitching);
        }

        // POST: SubmitPitchings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,Player,OpponentOfficialAtBats,OpponentHits,EarnedRunsScored,InningsPitched,StrikeOuts,HomeRuns,Walks,BattersHBP,PickOffAttempts,PickOffs")] SubmitPitching submitPitching)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submitPitching).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Player = new SelectList(db.Players, "PlayerID", "FirstName", submitPitching.Player);
            return View(submitPitching);
        }

        // GET: SubmitPitchings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            if (submitPitching == null)
            {
                return HttpNotFound();
            }
            return View(submitPitching);
        }

        // POST: SubmitPitchings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubmitPitching submitPitching = db.SubmitPitchings.Find(id);
            db.SubmitPitchings.Remove(submitPitching);
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