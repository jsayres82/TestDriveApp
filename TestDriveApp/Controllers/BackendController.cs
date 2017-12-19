using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDriveApp.Models;

namespace TestDriveApp.Controllers
{
    public class BackendController : Controller
    {
        TestDriveDataContext db = new TestDriveDataContext();

        public class JsonEvent
        {
            public string id { get; set; }
            public string text { get; set; }
            public string start { get; set; }
            public string end { get; set; }
        }


        public ActionResult Events(DateTime? start, DateTime? end)
        {

            // SQL: SELECT * FROM [event] WHERE NOT (([end] <= @start) OR ([start] >= @end))
            var events = from ev in db.Events.AsEnumerable() where !(ev.End <= start || ev.Start >= end) select ev;

            var result = events
            .Select(e => new JsonEvent()
            {
                start = e.Start.ToString("s"),
                end = e.End.ToString("s"),
                text = e.Text,
                id = e.Id.ToString()
            })
            .ToList();

            return new JsonResult { Data = result };
        }

        public ActionResult Create(string start, string end, string text)
        {
            var toBeCreated = new Event
            {
                Start = Convert.ToDateTime(start),
                End = Convert.ToDateTime(end),
                Text = text
            };
            db.Events.InsertOnSubmit(toBeCreated);
            db.SubmitChanges();

            return new JsonResult { Data = new Dictionary<string, object> { { "id", toBeCreated.Id } } };

        }

        public ActionResult Move(int id, string newStart, string newEnd)
        {
            var toBeMoved = (from ev in db.Events where ev.Id == id select ev).First();
            toBeMoved.Start = Convert.ToDateTime(newStart);
            toBeMoved.End = Convert.ToDateTime(newEnd);
            db.SubmitChanges();

            return new JsonResult { Data = new Dictionary<string, object> { { "id", toBeMoved.Id } } };
        }

    }
}