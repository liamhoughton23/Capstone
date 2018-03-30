using CAPSTONE.Models;
using System;
using System.Collections.Generic;
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

        public JsonResult GetEvents()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var events = db.Calendar.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Calendar evnt)
        {
            var status = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (evnt.EventID > 0)
                {
                    var v = db.Calendar.Where(a => a.EventID == evnt.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = evnt.Subject;
                        v.Start = evnt.Start;
                        v.End = evnt.End;
                        v.Description = evnt.Description;
                        v.IsFullDay = evnt.IsFullDay;
                        v.Color = evnt.Color;

                    }
                }
                else
                {
                    db.Calendar.Add(evnt);
                    db.SaveChanges();
                }
                db.SaveChanges();
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