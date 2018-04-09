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
    public class TeamConfirmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: TeamConfirms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeamConfirms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,Code")] TeamConfirm teamConfirm)
        {
            if (ModelState.IsValid)
            {
                db.Code.Add(teamConfirm);
                db.SaveChanges();
                string user = User.Identity.GetUserId();
                var userRow = from row in db.Players where row.UserId == user select row;
                var userRowResult = userRow.FirstOrDefault();
                string lastCode = (from n in db.Code orderby n.Code descending select n.Code).FirstOrDefault();
                if (lastCode == userRowResult.Code)
                {
                    userRowResult.Member = true;
                    db.SaveChanges();
                    return RedirectToAction("WhichStat", "Players");
                }
                return View(teamConfirm);
            }

            return View(teamConfirm);
        }

    }
}
