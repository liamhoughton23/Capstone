using CAPSTONE.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAPSTONE.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult  PlayerCalendar()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Calendar> newList = new List<Calendar>();

                string user = User.Identity.GetUserId();                        
                var coachRow = from row in db.Coaches where row.UserId == user select row;
                var coachRowResult = coachRow.FirstOrDefault();
                int coachID = coachRowResult.CoachID;
                //var playerRow = from row in db.Players where row.UserId == user select row;
                //var playerRowResult = playerRow.FirstOrDefault();
                //int playerCoachID = playerRowResult.CoachID;
                        foreach (var item in db.Calendar)
                        {
                            if(item.CoachID == coachID)
                              {
                                  newList.Add(item);
                              }
                        }

                return new JsonResult { Data = newList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetEventsPlayer()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Calendar> newList = new List<Calendar>();

                string user = User.Identity.GetUserId();
                //var coachRow = from row in db.Coaches where row.UserId == user select row;
                //var coachRowResult = coachRow.FirstOrDefault();
                //int coachID = coachRowResult.CoachID;
                var playerRow = from row in db.Players where row.UserId == user select row;
                var playerRowResult = playerRow.FirstOrDefault();
                int playerCoachID = playerRowResult.CoachID;
                foreach (var item in db.Calendar)
                {
                    if (item.CoachID == playerCoachID)
                    {
                        newList.Add(item);
                    }
                }

                return new JsonResult { Data = newList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Calendar e)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Calendar.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.Color = e.Color;
                        v.CoachID = e.CoachID;
                        string user = User.Identity.GetUserId();
                        var coachRow = from row in dc.Coaches where row.UserId == user select row;
                        var coachRowResult = coachRow.FirstOrDefault();
                        int coachID = coachRowResult.CoachID;
                        v.CoachID = coachID;
                    }
                }
                else
                {
                    string user = User.Identity.GetUserId();
                    var coachRow = from row in dc.Coaches where row.UserId == user select row;
                    var coachRowResult = coachRow.FirstOrDefault();
                    int coachID = coachRowResult.CoachID;
                    e.CoachID = coachID;
                    dc.Calendar.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var v = db.Calendar.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    db.Calendar.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
                return new JsonResult { Data = new { status = status } };
        }
    }
}