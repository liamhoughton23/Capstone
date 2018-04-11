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
using CAPSTONE.HelperClasses;

namespace CAPSTONE.Controllers
{
    public class CoachesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Coaches
        public ActionResult Index()
        {
            return View(db.Coaches.ToList());
        }

        // GET: Coaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // GET: Coaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoachID,TeamName,FirstName,LastName,UserId,PhoneNumber,Code")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.GetUserId();
                var result = from row in db.Users where row.Id == user select row;
                var rowResult = result.FirstOrDefault();
                coach.UserId = user;
                coach.PhoneNumber = rowResult.PhoneNumber;
                coach.Code = GetCode();
                db.Coaches.Add(coach);
                db.SaveChanges();
                Message message = new Message();
                message.content = "Your code is: " + coach.Code;
                message.recipient = coach.PhoneNumber;
                HelperClasses.Twilio twilio = new HelperClasses.Twilio();
                twilio.Send(message, coach.PhoneNumber);
                return RedirectToAction("Home", "Coaches");
            }
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoachID,TeamName,FirstName,LastName,UserId,PhoneNumber,Code")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coach coach = db.Coaches.Find(id);
            db.Coaches.Remove(coach);
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

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult Home()
        {
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Users where row.Id == sameUser select row;
            var resultToUser = result.FirstOrDefault();
            return View(resultToUser);
        }

        public string GetCode()
        {
            Random rand = new Random();
            int firstNumber = rand.Next(1000, 9999);

            string stringCode = firstNumber.ToString();
            return stringCode;
        }

        public ActionResult EnterStats()
        {
            return View();
        }

        public ActionResult ChoseStats()
        {
            return View();
        }

        public ActionResult PitchStats()
        {
            List<PitchStats> pitch = new List<PitchStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.PitchStats)
            {
                if (item.CoachID == firstResult.CoachID)
                {
                    pitch.Add(item);
                }
            }
            return View(pitch);
        }

        public ActionResult OffStats()
        {
            List<OffenseStats> offense = new List<OffenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Offense)
            {
                if (item.CoachID == firstResult.CoachID)
                {
                    offense.Add(item);
                }
            }
            return View(offense);
        }

        public ActionResult Pitcher()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 1)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult Catcher()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 2)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult First()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 3)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult Second()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 4)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }
        public ActionResult Third()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 5)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult Short()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 6)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult Left()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 7)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult Center()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 8)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult Right()
        {
            List<DefenseStats> defense = new List<DefenseStats>();
            string sameUser = User.Identity.GetUserId();
            var result = from row in db.Coaches where row.UserId == sameUser select row;
            var firstResult = result.FirstOrDefault();
            foreach (var item in db.Defense)
            {
                if (item.CoachID == firstResult.CoachID && item.Position == 9)
                {
                    defense.Add(item);
                }
            }
            List<DefenseStats> sortedList = defense.OrderBy(p => p.FPCT).ToList();
            return View(sortedList);
        }

        public ActionResult DefStats()
        {
            return View();
        }



    }
}
