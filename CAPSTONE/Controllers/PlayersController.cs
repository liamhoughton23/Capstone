using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAPSTONE.Models;
using Microsoft.AspNet.Identity;

namespace CAPSTONE.Controllers
{
    public class PlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Players
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Coach);
            return View(players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "TeamName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerID,FirstName,LastName,CoachID,UserId,PhoneNumber,Code,Email,Memeber")] Player player)
        {
            string user = User.Identity.GetUserId();
            var result = from row in db.Users where row.Id == user select row;
            var rowResult = result.FirstOrDefault();
            player.UserId = user;
            player.PhoneNumber = rowResult.PhoneNumber;
            player.Email = rowResult.Email;
            player.Member = false;
            db.Players.Add(player);
            db.SaveChanges();
            var coachResult = from row in db.Coaches where row.CoachID == player.CoachID select row;
            var first = coachResult.FirstOrDefault();
            player.Code = first.Code;
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();
            //var coachRow = from row in db.Coaches where row.CoachID == player.CoachID select row;
            //var coachRowResult = coachRow.FirstOrDefault();
            //player.Code = coachRowResult.Code;
            //db.Entry(player).State = EntityState.Modified;
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "TeamName", player.CoachID);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerID,FirstName,LastName,CoachID")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "TeamName", player.CoachID);
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
