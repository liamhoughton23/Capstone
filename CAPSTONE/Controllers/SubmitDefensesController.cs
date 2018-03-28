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
    public class SubmitDefensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: SubmitDefenses
        public ActionResult Index()
        {
            var submitDefenses = db.SubmitDefenses.Include(s => s.Player);
            return View(submitDefenses.ToList());
        }

        // GET: SubmitDefenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitDefense submitDefense = db.SubmitDefenses.Find(id);
            if (submitDefense == null)
            {
                return HttpNotFound();
            }
            return View(submitDefense);
        }

        // GET: SubmitDefenses/Create
        public ActionResult Create()
        {
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName");
            return View();
        }

        // POST: SubmitDefenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,PlayerID,Positions,Errors,InningsPlayed,PutOuts,Assists")] SubmitDefense submitDefense, TotalDefense totalDefense)
        {
            TotalDefensesController total = new TotalDefensesController();
            SubmitPitchingsController pitch = new SubmitPitchingsController();
            //SubmitPitching pitching = new SubmitPitching();
            //TotalPitching totalPitch = new TotalPitching();
            if (ModelState.IsValid)
            {
                db.SubmitDefenses.Add(submitDefense);
                db.SaveChanges();
                foreach (var item in db.TotalDefenses)
                {
                    if (item.Positions == submitDefense.Positions && item.PlayerID == submitDefense.PlayerID && submitDefense.Positions != 1)
                    {
                        total.Edit(submitDefense.PlayerID, submitDefense.Positions, submitDefense.Errors, submitDefense.InningsPlayed, submitDefense.PutOuts, submitDefense.Assists);
                        if (submitDefense.Positions == 1)
                        {
                            //pitch.Create(pitching, totalPitch);
                            return RedirectToAction("Create", "SubmitPitchings");
                        }
                        return RedirectToAction("Index", "DefenseStats");
                    }
                    //else if (submitDefense.Positions == 1)
                    //{
                    //    //pitch.Create(pitching, totalPitch);
                    //    return RedirectToAction("Create", "SubmitPitchings");
                    //}
                }
            total.Create(submitDefense.PlayerID, submitDefense.Positions, submitDefense.Errors, submitDefense.InningsPlayed, submitDefense.PutOuts, submitDefense.Assists, totalDefense);
                if (submitDefense.Positions == 1)
                {
                    //pitch.Create(pitching, totalPitch);
                    return RedirectToAction("Create", "SubmitPitchings");
                }
            return RedirectToAction("Index", "DefenseStats");
            }
            return RedirectToAction("Index", "DefenseStats");
            //ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", submitDefense.PlayerID);
            //return View(submitDefense);
        }

        // GET: SubmitDefenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitDefense submitDefense = db.SubmitDefenses.Find(id);
            if (submitDefense == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", submitDefense.PlayerID);
            return View(submitDefense);
        }

        // POST: SubmitDefenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,PlayerID,Attempts,Errors,InningsPlayed,PutOuts,Assists")] SubmitDefense submitDefense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submitDefense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "FirstName", submitDefense.PlayerID);
            return View(submitDefense);
        }

        // GET: SubmitDefenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitDefense submitDefense = db.SubmitDefenses.Find(id);
            if (submitDefense == null)
            {
                return HttpNotFound();
            }
            return View(submitDefense);
        }

        // POST: SubmitDefenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubmitDefense submitDefense = db.SubmitDefenses.Find(id);
            db.SubmitDefenses.Remove(submitDefense);
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
