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



        public ActionResult LocationIndex()
        {
            return View();
        }


        // GET: Players
        public ActionResult Index()
        {
            List<Player> player = new List<Player>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Players)
            {
                if (item.CoachID == firstResult.CoachID)
                {
                    player.Add(item);
                }
            }
            return View(player);
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
            return RedirectToAction("Login", "Account");
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

        public ActionResult PlayerDefense()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Players where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            var stats = from row in db.Defense where row.PlayerID == firstResult.PlayerID select row;
            var statsFirst = stats.FirstOrDefault();
            foreach(var item in db.Defense)
            {
                if (item.PlayerID == statsFirst.PlayerID)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            sortedList.Reverse();

            return View(sortedList);
        }

        public ActionResult WhichStat()
        {
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Players where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            if (firstResult.Member == false)
            {
                return RedirectToAction("Create", "TeamConfirms");
            }
            return View(firstResult);
        }

        public ActionResult Home()
        {
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Users where row.Id == sameUser select row;
            var resultToUser = result.FirstOrDefault();
            return View(resultToUser);
        }

        public ActionResult PlayerOffense()
        {
            List<OffenseStats> offense = new List<OffenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Players where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Offense)
            {
                if (item.CoachID == firstResult.CoachID)
                {
                    offense.Add(item);
                }
            }
            List<OffenseStats> sortedList = offense.OrderBy(p => p.BA).ToList();
            sortedList.Reverse();
            return View(sortedList);
    }

        public ActionResult PlayerPitch()
        {
            List<PitchStats> pitch = new List<PitchStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Players where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.PitchStats)
            {
                if (item.PlayerID == firstResult.PlayerID)
                {
                    pitch.Add(item);
                }
            }


            return View(pitch);
        }

        public ActionResult LineUp()
        {
            List<LineUp> lineUp = new List<LineUp>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Players where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Lineups)
            {
                if (item.PlayerID == firstResult.PlayerID)
                {
                    lineUp.Add(item);
                }
            }

            return View(lineUp);
        }
    }
}
