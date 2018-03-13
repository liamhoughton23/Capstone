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
    public class LineUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LineUps
        public ActionResult Index()
        {
            return View(db.Lineups.ToList());
        }

        // GET: LineUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.Lineups.Find(id);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            return View(lineUp);
        }

        // GET: LineUps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LineUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LeadOff,LeadOffPosition,SecondHitter,TwoHitPosition,ThreeHitter,ThreePosition,FourHitter,Fourposition,FiveHitter,FivePosition,SixHitter,SixPosition,SevenHitter,SevenPosition,EightHitter,EightPosition,NineHitter,NinePosition")] LineUp lineUp)
        {
            if (ModelState.IsValid)
            {
                db.Lineups.Add(lineUp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lineUp);
        }

        // GET: LineUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.Lineups.Find(id);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            return View(lineUp);
        }

        // POST: LineUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LeadOff,LeadOffPosition,SecondHitter,TwoHitPosition,ThreeHitter,ThreePosition,FourHitter,Fourposition,FiveHitter,FivePosition,SixHitter,SixPosition,SevenHitter,SevenPosition,EightHitter,EightPosition,NineHitter,NinePosition")] LineUp lineUp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lineUp);
        }

        // GET: LineUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineUp lineUp = db.Lineups.Find(id);
            if (lineUp == null)
            {
                return HttpNotFound();
            }
            return View(lineUp);
        }

        // POST: LineUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineUp lineUp = db.Lineups.Find(id);
            db.Lineups.Remove(lineUp);
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
